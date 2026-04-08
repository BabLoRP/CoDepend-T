#!/usr/bin/env bash
set -e

PYTHON=""

if py -3.10 --version &>/dev/null 2>&1; then
    PYTHON="py -3.10"
elif python3.10 --version &>/dev/null 2>&1; then
    PYTHON="python3.10"
elif python3 --version 2>&1 | grep -q "3\.10"; then
    PYTHON="python3"
elif python --version 2>&1 | grep -q "3\.10"; then
    PYTHON="python"
else
    echo "Error: Python 3.10 not found. Install it from https://www.python.org/downloads/release/python-31011/"
    exit 1
fi

echo "Using: $($PYTHON --version)"
echo "Creating virtual environment in ./.venv ..."
$PYTHON -m venv .venv

echo "Installing ArchLens ..."
if [ -f ".venv/Scripts/pip" ]; then
    .venv/Scripts/pip install --quiet ArchLens
else
    .venv/bin/pip install --quiet ArchLens
fi

echo ""
echo "Done with setting up ArchLens."
