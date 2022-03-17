﻿using System;

namespace DAL.DAL
{
    public enum MuscleType
    {
        Neck = 1,
        FrontDelt = 2,
        SideDelt = 3,
        RearDelt = 4,
        Trap = 5,
        Lats = 6,
        LowBack = 7,
        Bicep = 8,
        Brachialis = 9,
        Tricep = 10,
        Forearm = 11,
        Pectoral = 12,
        Abs = 13,
        Side = 14,
        Glute = 15,
        Quad = 16,
        LegBicep = 17,
        Abductor = 18,
        Adductor = 19,
        Calve = 20,
        Heart = 21
    }

    public enum MovementType
    {
        Push = 1,
        Pull = 2,
        Leg = 3,
        Cardio = 4
    }

    public enum BodySectionType
    {
        Upper = 1,
        Lower = 2
    }

    public enum BodyPartType
    {
        Neck = 1,
        Chest = 2,
        Back = 3,
        Shoulder = 4,
        Arm = 5,
        Abs = 6,
        Glute = 7,
        Leg = 8,
        Heart = 9
    }
}