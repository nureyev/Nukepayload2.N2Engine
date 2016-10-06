﻿Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.Elements

Friend Class PolylineElementRenderer
    Sub New(view As PolylineElement)
        MyBase.New(view)
    End Sub

    Protected Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim loc = View.Location.Value.AsXnaVector2
        args.DrawingContext.DrawPolyline(View.Points.Value.Select(Function(p) loc + p.AsXnaVector2).ToArray,
                                         View.Stroke.Value.AsXnaColor)
    End Sub
End Class