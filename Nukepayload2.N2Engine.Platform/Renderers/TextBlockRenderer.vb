﻿Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.Platform
''' <summary>
''' 文字渲染器
''' </summary>
<PlatformImpl(GetType(Nukepayload2.N2Engine.UI.Controls.TextBlock))>
Partial Friend Class TextBlockRenderer
    Inherits GameElementRenderer

    Sub New(tbl As Nukepayload2.N2Engine.UI.Controls.TextBlock)
        MyBase.New(tbl)
    End Sub

End Class
