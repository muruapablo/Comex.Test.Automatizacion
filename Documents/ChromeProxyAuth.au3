WinWaitActive("","Chrome Legacy Window", "120")
If WinExists("","Chrome Legacy Window") Then
Send("SF77332{TAB}")
Send("Jul*Arg$2025{Enter}")
EndIf