﻿Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.ParticleSystems
Imports Nukepayload2.N2Engine.UI.ParticleSystemViews

Friend Class SmokeParticleSystemRenderer

    Sub New(view As SmokeParticleSystemView)
        MyBase.New(view)
    End Sub

    Friend Overrides Async Function CreateResourceAsync(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs) As Task
        Dim view = DirectCast(MyBase.View, SmokeParticleSystemView)
        Await SpriteParticleSystemHelper.LoadAsync(Of SpriteParticle, SmokeParticleSystem)(sender, args, view)
    End Function

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim drawingSession = args.DrawingSession
        Dim view = DirectCast(MyBase.View, SmokeParticleSystemView)
        SpriteParticleSystemHelper.Draw(Of SpriteParticle, SmokeParticleSystem)(drawingSession, view)
    End Sub

End Class