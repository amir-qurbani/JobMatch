// ===== PARAMETERS =====
param webAppName string
param location string = resourceGroup().location
param sku string = 'F1'
param sqlServerName string
param sqlAdminLogin string
@secure()
param sqlAdminPassword string

// ===== VARIABLES =====
var appServicePlanName = '${webAppName}-plan'

// ===== RESOURCES =====
resource appServicePlan 'Microsoft.Web/serverfarms@2023-01-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: sku
  }
}
resource webapp 'Microsoft.Web/sites@2023-01-01' = {
  name: webAppName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
  }
}
resource sqlServer 'Microsoft.Sql/servers@2023-05-01-preview' = {
  name: sqlServerName
  location: location
  properties: {
    administratorLogin: sqlAdminLogin
    administratorLoginPassword: sqlAdminPassword
  }
}
resource sqlDatabase 'Microsoft.Sql/servers/databases@2023-05-01-preview' = {
  parent: sqlServer
  name: 'JobMatchDb'
  location: location
  sku: {
    name: 'Basic'
    tier: 'Basic'
  }
}
resource sqlFirewall 'Microsoft.Sql/servers/firewallRules@2023-05-01-preview' = {
  parent: sqlServer
  name: 'AllowAzureServices'
  properties: {
    startIpAddress: '0.0.0.0'
    endIpAddress: '0.0.0.0'
  }
}

// ===== OUTPUTS =====
output webAppUrl string = 'https://${webapp.properties.defaultHostName}'
output sqlServerFqdn string = sqlServer.properties.fullyQualifiedDomainName
