param (
    [string]$testType,
    [switch]$batchMode,
    [switch]$enableCodeCoverage
)

# The directory that houses the Unity.exe folder
$UnityPath = $Env:UnityPath

# Matches the product name in the Unity build settings
$ProjectName = "ProceduralToolkit"

# Get the current directory
$CurrentPath = (Get-Item -Path ".\").FullName

# Test results file
$ResultsPath = "$CurrentPath\TestResults`\$testType.results.xml"

# Unity test runner test filter
$TestFilter = "$ProjectName.EditorTests..*$testType"

# Unity batchmode run
$BatchModeStr = if ($batchMode) { "-batchmode" } else { "" }

# Log file name
$LogFile = "$CurrentPath\Logs`\$testType.log"

# Code coverage string
$CodeCoverageStr = ""
if ($enableCodeCoverage)
{
    # Code coverage results path
    $CoverageResultsPath = "CodeCoverage"

    # Code coverage assemblies
    $CoverageAssemblies = "+$ProjectName"

    # Code coverage filters
    $CoverageFilters = "-*/Components/*,-*/UI/*,-*/MenuEntries.cs"

    # Code coverage options
    $CoverageOptions = "enableCyclomaticComplexity;assemblyFilters:$CoverageAssemblies;pathFilters:$CoverageFilters"

    # Update code coverage string
    $CodeCoverageStr = "-enableCodeCoverage -coverageResultsPath `"$CoverageResultsPath`" -coverageOptions $CoverageOptions"

    # Code coverage OpenCover xml results path
    $CoverageOpenCoverPath = "$CoverageResultsPath\*-opencov\EditMode\*.xml"
}

# Unity test runner arguments
$UnityArgs = "
                -runTests
                $BatchModeStr
                -projectPath `"$CurrentPath`"
                -forgetProjectPath
                -testFilter `"$TestFilter`"
                -testResults `"$ResultsPath`"
                -logFile $LogFile
                $CodeCoverageStr
             "

# Verify we can find the unity executable
Write-Host "Verifying that UnityPath is set: " -NoNewline
if (-not (Test-Path "$UnityPath"))
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
if (-not (Test-Path "$ResultsPath"))
{
    Write-Host "Error."
    Write-Error "Could not locate results file $ResultsPath."
    exit 2
}
Write-Host "Ok."

# Parsing test results
unity-testresult-parser --color yes --summary $ResultsPath
if (-not $?)
{
    Write-Error "Some tests failed."
    exit 3
}

# Converting OpenCover to Cobertura
if ($enableCodeCoverage)
{
    if (-not (Test-Path "$CoverageOpenCoverPath"))
    {
        Write-Warning "Was not able to find coverage results"
    }
    else
    {
        
        $Reports = "-reports:$CoverageOpenCoverPath"
        $TargetDir = "-targetdir:$CoverageResultsPath"
        $ReportTypes = "-reporttypes:Cobertura"
        $SourceDirs = "-sourcedirs:$CurrentPath\Packages\com.daniil-ryzhkov.proceduraltoolkit"
        
        ReportGenerator.exe $Reports $TargetDir $ReportTypes $SourceDirs
    }
}