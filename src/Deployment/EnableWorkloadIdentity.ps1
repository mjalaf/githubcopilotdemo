$resourceGroup="RG-GaliciaPoc-dev-eastus2-001"
$aksName="aks-poc-dev-eastus2-001"
$kvName="kv-poc-dev-eastus2-001"
$workloadIdentity="workload-identity"

# update aks cluster
az aks update -g $resourceGroup -n $aksName --enable-oidc-issuer --enable-workload-identity

# get aks cluster credentials
az aks get-credentials --resource-group $resourceGroup --name $aksName

# get the OIDC Issuer URL
$oidcUrl="$(az aks show --name $aksName --resource-group $resourceGroup --query "oidcIssuerProfile.issuerUrl" -o tsv)"

# managed identity for workload identity
az identity create --name $workloadIdentity --resource-group $resourceGroup

# Grant permission to access the secret in Key Vault
$clientId=$(az identity show --name $workloadIdentity --resource-group $resourceGroup --query clientId -o tsv)
az keyvault set-policy -n $kvName --spn $clientId --secret-permissions get

# Create federated identity credentials )
az identity federated-credential create --name "aks-federated-credential" --identity-name $workloadIdentity --resource-group $resourceGroup --issuer "${oidcUrl}" --subject "system:serviceaccount:default:workload-sa"



