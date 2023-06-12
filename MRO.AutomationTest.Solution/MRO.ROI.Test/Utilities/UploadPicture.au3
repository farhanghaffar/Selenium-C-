#Region ;**** Directives created by AutoIt3Wrapper_GUI ****
#AutoIt3Wrapper_UseX64=y
#EndRegion ;**** Directives created by AutoIt3Wrapper_GUI ****

$hWnd = WinGetHandle("Open")
ControlSend($hWnd, "", "Edit1", $CmdLine[1])
Sleep(1000)
ControlClick($hWnd, "", "Button1")