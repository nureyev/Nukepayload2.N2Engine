﻿Imports Nukepayload2.N2Engine.Platform

Namespace Global.Nukepayload2.N2Engine.Storage

    ''' <summary>
    ''' 存档管理器的平台实现
    ''' </summary>
    <PlatformImpl(GetType(SaveManager))>
    Partial Friend Class SaveManagerImpl
        Inherits PlatformSaveManagerBase

    End Class
End Namespace
