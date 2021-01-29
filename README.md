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

### Reference

- `MemberData`/`TheoryData` in tests: 
  - https://github.com/pattons-cottam/Advent-of-Code-2020/blob/d08f3388b8a7d4beca301b8c07db033e20674ae6/2.%20Password%20Philosophy/PasswordPhilosophy.Tests/PasswordPhilosophyTests.cs#L15
