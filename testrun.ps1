param ([string]$testType)

# The directory that houses the Unity.exe folder
$UnityPath = $Env:UnityPath

# Matches the product name in the Unity build settings
$ProjectName = "ProceduralToolkit"

# Get the current directory
$CurrentPath = (Get-Item -Path ".\").FullName

# Test results file
$ResultsPath = "$CurrentPath\results.xml"

# Unity test runner test filter
$TestFilter = "$ProjectName.EditorTests..*$testType"

# Unity test runner arguments
$UnityArgs = "
                -runTests
                -batchmode
                -projectPath `"$CurrentPath`"
                -forgetProjectPath
                -testFilter `"$TestFilter`"
                -testResults `"$ResultsPath`"
                -logFile stdout.log
             "

# Verify we can find the unity executable
Write-Host "Verifying that UnityPath is set: " -NoNewline
if(-not (Test-Path "$UnityPath"))
{
    Write-Host "Error."
    Write-Error "Could not locate Unity executable using path $UnityPath."
    exit 1
}
Write-Host "Ok."

# Start Unity and tell it to run the unit tests
Write-Host "Starting Unity test run process..."
$unityProcess = Start-Process -FilePath $UnityPath -ArgumentList $UnityArgs -PassThru
Wait-Process -InputObject $unityProcess

# Verify we can find the results xml file
Write-Host "Verifying test results file created: " -NoNewline
if(-not (Test-Path "$ResultsPath"))
{
    Write-Host "Error."
    Write-Error "Could not locate results file $ResultsPath."
    exit 2
}
Write-Host "Ok."

unity-testresult-parser --color yes --summary $CurrentPath\results.xml
if (-not $?)
{
    Write-Error "Some tests failed."
    exit 3
}
