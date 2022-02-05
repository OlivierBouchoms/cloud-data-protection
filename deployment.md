# Services

## Kubernetes

| Name                  | Host/port                 | External  | 
| -------------         | -----                     | ---       | 
| Frontend              | loadbalancer:80           | ✅        | 
| Gateway               | loadbalancer:5001         | ✅        |
| BackupConfiguration   | backup-config-cluster-ip  | ❌        | 
| Onboarding            | onboarding-cluster-ip     | ❌        |
| Mail                  | none                      | ❌        |

# Secrets

## Kubernetes

| Name in code                                      | Name in k8s                                   |
| -------------                                     | -----                                         |
| CDP_ONBOARDING_GOOGLE_OAUTH2_CLIENT_ID            | cdp-onboarding-google-oauth2-client-id        |
| CDP_ONBOARDING_GOOGLE_OAUTH2_CLIENT_SECRET        | cdp-onboarding-google-oauth2-client-secret    |
| CDP_SENDGRID                                      | cdp-sendgrid                                  |
| CDP_SENDGRID_SENDER                               | cdp-sendgrid-sender                           |
| CDP_BACKUP_DEMO_FUNCTIONS_KEY                     | cdp-backup-demo-api-key                       |
| CDP_PAPERTRAIL_ACCESS_TOKEN                       | cdp-papertrail-access-token                   |
| CDP_PAPERTRAIL_URL                                | cdp-papertrail-url                            |

## Functions

| Name in code              | Name in function          |
| ---                       | ---                       |
| CDP_DEMO_BLOB_AES_IV      | CDP_DEMO_BLOB_AES_IV      |
| CDP_DEMO_BLOB_AES_KEY     | CDP_DEMO_BLOB_AES_KEY     |
| CDP_DEMO_BLOB_CONNECTION  | CDP_DEMO_BLOB_CONNECTION  |
| CDP_BACKUP_DEMO_API_KEY   | CDP_BACKUP_DEMO_API_KEY   |
