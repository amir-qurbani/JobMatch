#Provider - vilket moln vi använder
terraform {
    required_providers {
        azurerm = {
            source = "hashicorp/azurerm"
            version = "~> 3.0"
        }
    }
}
#Konfiguration Azure-providern
provider "azurerm" {
    features {}
}
# Varibales - värden som kan ändras
variable "resource_group_name" {
    default = "rg-jobmatch-tf"
}
variable "location" {
    default = "swedencentral"
}
variable "web_app_name" {
    default = "jobmatch-api-tf"
}
variable "sql_admin_login"{
    sensitive = true
}
variable "sql_admin_password" {
    sensitive = true
}

# Skapa Resource Group i Azure
resource "azurerm_resource_group" "main" {
    name = var.resource_group_name
    location = var.location
}
#App Service Plan - servern som kör appen 
resource "azurerm_service_plan" "main" {
    name = "jobmatch-plan-tf"
    resource_group_name = azurerm_resource_group.main.name
    location = azurerm_resource_group.main.location
    os_type = "Linux"
    sku_name = "F1"
}
# Weba App - din .Net API 
resource "azurerm_linux_web_app" "main"{
    name = var.web_app_name
    resource_group_name = azurerm_resource_group.main.name
    location = azurerm_resource_group.main.location
    service_plan_id = azurerm_service_plan.main.id

    site_config{
        always_on = false
        application_stack {
            dotnet_version = "8.0"
        }
    }
    connection_string {
    name = "DefaultConnection"
    type = "SQLAzure"
    value = "Server=${azurerm_mssql_server.main.fully_qualified_domain_name};Database=JobMatchDb;User Id=${var.sql_admin_login};Password=${var.sql_admin_password};Encrypt=true;"
    }
}
# Sql - servern
resource "azurerm_mssql_server" "main"{
    name = "jobmatch-sql-tf"
    resource_group_name = azurerm_resource_group.main.name
    location = azurerm_resource_group.main.location
    version = "12.0"
    administrator_login = var.sql_admin_login
    administrator_login_password = var.sql_admin_password
}
#Sql - database
resource "azurerm_mssql_database" "main" {
    name = "JobMatchDb"
    server_id = azurerm_mssql_server.main.id
    sku_name = "Basic"
}
# Firewall - tillåter azure tjänster att nå sql server
resource "azurerm_mssql_firewall_rule" "allow_azure" {
    name = "AllowAzureServices"
    server_id = azurerm_mssql_server.main.id
    start_ip_address = "0.0.0.0"
    end_ip_address = "0.0.0.0"
}
