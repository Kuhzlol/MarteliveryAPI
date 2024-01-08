## Setting up the secret manager
```powershell
dotnet user-secrets init --project .\MarteliveryAPI.csproj
```

## Setting the connection string to secret manager
```powershell
dotnet user-secrets set "ConnectionStrings:CONNECTIONSTRINGNAME" "Host=HOSTNAME;Port=PORTNUMBER;Database=DBNAME;Username=USERNAME;Password=PASSWORD" --project PROJECTNAMEPATH
```

## Listing the secrets from secret manager
```powershell
dotnet user-secrets list
```

## Latest Key Vault Syntax
```powershell
@Microsoft.KeyVault(SecretUri=https://myvault.vault.azure.net/secrets/mysecret/)

@Microsoft.KeyVault(VaultName=myvault;SecretName=mysecret)
```