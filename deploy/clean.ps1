Write-Host "About to clean: " $args[0]
if (Test-Path $args[0]) {
    ri $args[0] -Recurse -Force
}

