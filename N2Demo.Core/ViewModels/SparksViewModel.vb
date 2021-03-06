﻿Imports System.Numerics
Imports Newtonsoft.Json
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.N2Math
Imports Nukepayload2.N2Engine.ParticleSystems

Public Class SparksViewModel
    Inherits SingleInstance(Of SparksViewModel)

    <JsonIgnore>
    Dim _predefinedColor As Color() = {Colors.Orange, Colors.Gold, Colors.Yellow}

    Public Property SparkSys As New SparkParticleSystem(1000, Integer.MaxValue, 30, 150) With {
        .Location = New Vector2(150, 150), .GenerateColor = Function() _predefinedColor(RandomGenerator.Rand.Next(0, 3))
    }

    <JsonIgnore>
    Public Property GhostFlameSys As SwarmParticleSystem

    Public ReadOnly Property GhostFlameSheet As New GhostFlameSpriteSheet

    Public Property RedCircle As New ColorizedBound With {
        .Position = New Vector2(130, 200),
        .Color = Colors.Red,
        .Size = New Vector2(100, 100)
    }
    Public Property GreenRectangle As New ColorizedBound With {
        .Position = New Vector2(330, 200),
        .Color = Colors.Green,
        .Size = New Vector2(233, 100)
    }

    Public ReadOnly Property ElderText As String = $"苟利国家生死以，岂因祸福避趋之。
暴力膜不可取。"

    Public Property ShakingViewer As New ShakingViewer

    Public ReadOnly Property CharacterSheet As New CharacterSheet

    <JsonIgnore>
    Public Property PressedKeyCount As Integer

    <JsonIgnore>
    Public Property LastMouseState As String = "无"

    <JsonIgnore>
    Public Property LastTouchState As String = "无"

    Public Property ButtonStatus As New ButtonStatus

End Class