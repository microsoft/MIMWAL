@echo off

@echo .
@echo NOTE: This script needs to be run with admin previledges
@echo .

setlocal
set zWorkDir=%~dp0
pushd %zWorkDir%

set logmanTraceFile=%zWorkDir%FIMETWtrace.etl
set logmanStd=%zWorkDir%logman.out
set tracerptOutFileCSV=%zWorkDir%FIMTrace.csv
set tracerptOutFileEVTX=%zWorkDir%FIMTrace.evtx
set tracerptSummary=%zWorkDir%FIMTraceSummary.txt 
set tracerptReport=%zWorkDir%FIMTraceReport.xml 

logman start mysession -p {C2751E84-AD11-4a18-9507-6CFE811D3506} -o "%logmanTraceFile%" -ets > "%logmanStd%"

if {%errorlevel%} NEQ {0} (echo Error 0x%=exitcode%: logman command failed. Check details in: "%logmanStd%".) & (goto exit_error)

@echo .
@echo Please repeate the FIM task to capture the trace and wait for FIM processing to finish. Then press any key to end trace capture.
@echo .

@pause

logman stop mysession -ets >> "%logmanStd%"

del "%tracerptOutFileCSV%"
del "%tracerptOutFileEVTX%"
del "%tracerptSummary%"
del "%tracerptReport%"

tracerpt "%logmanTraceFile%" -o "%tracerptOutFileCSV%" -of CSV -summary "%tracerptSummary%" -report "%tracerptReport%"
tracerpt FIMETWtrace.etl -o "%tracerptOutFileEVTX%" -of EVTX

:exit_error
echo Aborting script execution...

:end_of_file
popd
endlocal
@pause
