# ASimmo

ASimmo est une application web ASP.NET Core MVC pour la gestion de biens immobiliers, d'adresses, de classifications et d'entités associées. La solution comprend l'application principale ainsi qu'une suite de tests automatisés.

## Structure du projet

- **ASimmo/** : Application principale ASP.NET Core MVC
    - Contrôleurs, modèles, vues, données et configuration de l'identité
    - Gestion des annonces immobilières, des adresses, de l'authentification des utilisateurs et du contrôle d'accès par rôle
- **ASimmo.Tests/** : Projet de tests automatisés
    - Tests fonctionnels et de fumée pour les contrôleurs et les pages
    - Utilise xUnit et l'infrastructure de test serveur d'ASP.NET Core

## Démarrage rapide

### Prérequis
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- (Optionnel) SQLite ou un autre fournisseur de base de données

### Installation
1. **Cloner le dépôt**
2. **Restaurer les dépendances :**
   ```bash
   dotnet restore
   ```
3. **Appliquer les migrations et initialiser la base de données :**
   ```bash
   dotnet ef database update
   ```
   La base sera initialisée avec des utilisateurs et rôles par défaut définis dans `appsettings.json` et `Startup.cs`.
4. **Lancer l'application :**
   ```bash
   dotnet run --project ASimmo
   ```
   L'application sera accessible sur `https://localhost:5001` ou `http://localhost:5000` par défaut.

### Utilisateurs par défaut
Les utilisateurs et rôles sont définis dans `appsettings.json`. Exemples d'identifiants :
- **Admin** : `admin@email.com` / `P@ssw0rd`
- **Agent** : (voir `appsettings.json` pour le détail)

Ces utilisateurs permettent d'accéder aux routes protégées par rôle.

## Exécution des tests

Placez-vous à la racine de la solution et lancez :
```bash
dotnet test
```

Cela exécutera tous les tests fonctionnels et de fumée dans `ASimmo.Tests`. Certains tests s'authentifient avec les utilisateurs par défaut pour vérifier l'accès aux routes protégées.

## Fonctionnalités principales
- Gestion des biens immobiliers
- CRUD sur les adresses
- Contrôle d'accès par rôle (Admin, Agent, etc.)
- Intégration ASP.NET Core Identity
- Tests fonctionnels et de fumée automatisés

## Remarques
- L'infrastructure de test inclut des helpers pour s'authentifier avec les utilisateurs par défaut et accéder aux routes protégées.
- Si vous ajoutez de nouvelles routes protégées, adaptez les tests pour utiliser le helper d'authentification.

## Licence
MIT
