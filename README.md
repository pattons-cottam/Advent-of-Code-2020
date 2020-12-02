# Advent-of-Code-2020

### How to generate a solution in this format:

```ps
dotnet new sln -n SolutionName
dotnet new console -o ProjectName
dotnet sln add ProjectName/ProjectName.csproj
dotnet new xunit -o ProjectName.Tests
dotnet sln add ProjectName.Tests/ProjectName.Tests.csproj
# change to tests directory
dotnet add reference ../ProjectName/ProjectName.csproj
```