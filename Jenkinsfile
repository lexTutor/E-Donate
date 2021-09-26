node {
    agent any
    triggers
        {
            pollSCM('* * * * *')
        }
    stage('Checkout') { 
        // for display purposes
        //Implicit checkout: Already mapped to the branch from Jenkins
    stage('Build')
        {
            bat 'C:/ProgramData/chocolatey/lib/NuGet.CommandLine/tools/nuget.exe restore PaymentService-Framework.sln'
            bat "\"${tool 'MSBuild-Local'}\" PaymentService-Framework.sln /p:Configuration=Debug /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
        }
    stage('Test')
        {
            bat 'C:/ProgramData/chocolatey/lib/XUnit/tools/xunit.console.exe PaymentService.API.IntegrationTest/bin/Debug/net472/PaymentService.API.IntegrationTest.dll'
        }
}
