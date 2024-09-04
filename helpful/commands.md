# migraton
in `src` directory: 
    dotnet ef migrations add UpdateDutchGameAndDutchScoreRelationship --project AllStars.Infrastructure --startup-project AllStars.API
    dotnet ef database update --verbose --project AllStars.Infrastructure --startup-project AllStars.API