﻿Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.Elements

Friend Class BezierCurveCustomElementRenderer
    Sub New(view As BezierCurveCustomElement)
        MyBase.New(view)
    End Sub

    Protected Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim loc = View.Location.Value.AsXnaVector2
        args.DrawingContext.DrawBezier(loc + View.StartPoint.Value.AsXnaVector2,
                                       loc + View.EndPoint.Value.AsXnaVector2,
                                       View.Stroke.Value.AsXnaColor, Function(t) loc + View.GetVertex(t).AsXnaVector2)
    End Sub
End Class