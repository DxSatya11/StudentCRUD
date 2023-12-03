param webAppName string // Generate unique String for web app name
param sku string = 'F1' // The SKU of App Service Plan
//param linuxFxVersion string = 'DOTNETCORE|7.0' // The runtime stack of web app
param dotnetVersion string = 'v7'
param location string = resourceGroup().location // Location for all resources
param appServicePlanName string

resource appServicePlan 'Microsoft.Web/serverfarms@2020-06-01' = {
  name: appServicePlanName
  location: location
  properties: {
    reserved: true
  }
  sku: {
    name: sku
  }
  kind: 'app'
}

resource appService 'Microsoft.Web/sites@2020-06-01' = {
  name: webAppName
  location: location
  kind:'app'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
       netFrameworkVersion: dotnetVersion 
     }
  }
}
