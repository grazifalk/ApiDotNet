mkdir .coverage

dotnet test ApiDotNet.UnitTest/ApiDotNet.UnitTest.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput="../.coverage/Data.opencover.xml" 
reportgenerator "-reports:.coverage/Data.opencover.xml" "-targetdir:.coverage/.coverage-report" -reporttypes:Html

PAUSE