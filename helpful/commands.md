# migraton
in `backend/src` directory (App must NOT be running): 
    dotnet ef migrations add Name --project AllStars.Infrastructure --startup-project AllStars.API
    dotnet ef database update --verbose --project AllStars.Infrastructure --startup-project AllStars.API

okazjonalnie:
    dotnet build --project AllStars.Infrastructure --startup-project AllStars.API