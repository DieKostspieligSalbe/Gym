using DAL.DAL;
using System.Collections.Generic;

namespace Gym.Services
{
    public static class MuscleStorage
    {
        public static List<MuscleDAL> muscleList { get; set; } = new();

        static MuscleStorage()
        {
            MuscleDAL neck = new() { Name = "Neck", BodyPartType = BodyPartType.Neck, BodySectionType = BodySectionType.Upper, MuscleType = MuscleType.Neck, MovementType = MovementType.Push };
            MuscleDAL trap = new() { Name = "Trapezius", MuscleType = MuscleType.Trap, MovementType = MovementType.Pull, BodyPartType = BodyPartType.Back, BodySectionType = BodySectionType.Upper };
            MuscleDAL frontDelt = new() { Name = "Front Deltoid", BodySectionType = BodySectionType.Upper, BodyPartType = BodyPartType.Shoulder, MovementType = MovementType.Push, MuscleType = MuscleType.FrontDelt };
            MuscleDAL sideDelt = new() { Name = "Middle Deltoid", MovementType = MovementType.Push, BodyPartType = BodyPartType.Shoulder, BodySectionType = BodySectionType.Upper, MuscleType = MuscleType.SideDelt };
            MuscleDAL rearDelt = new() { Name = "Rear Deltoid", MuscleType = MuscleType.RearDelt, BodySectionType = BodySectionType.Upper, BodyPartType = BodyPartType.Shoulder, MovementType = MovementType.Push };
            MuscleDAL infraspin = new() { Name = "Infraspinatus", MovementType = MovementType.Push, BodyPartType = BodyPartType.Back, BodySectionType = BodySectionType.Upper, MuscleType = MuscleType.Infraspinatus };
            MuscleDAL lats = new() { Name = "Latissimus Dorsi", MuscleType = MuscleType.Lats, BodySectionType = BodySectionType.Upper, BodyPartType = BodyPartType.Back, MovementType = MovementType.Pull };
            MuscleDAL lowBack = new() { Name = "Lower back", MovementType = MovementType.Push, BodyPartType = BodyPartType.Back, BodySectionType = BodySectionType.Upper, MuscleType = MuscleType.LowBack };
            MuscleDAL obliques = new() { Name = "Oblique", MuscleType = MuscleType.Oblique, BodySectionType = BodySectionType.Upper, BodyPartType = BodyPartType.Abdominals, MovementType = MovementType.Pull }; //TYPE
            MuscleDAL abs = new() { Name = "Abdominals", MovementType = MovementType.Pull, BodySectionType = BodySectionType.Upper, BodyPartType = BodyPartType.Abdominals, MuscleType = MuscleType.Abs };
            MuscleDAL pecs = new() { Name = "Pectoralis", MovementType = MovementType.Push, BodySectionType = BodySectionType.Upper, BodyPartType = BodyPartType.Chest, MuscleType = MuscleType.Pectoral };
            MuscleDAL heart = new() { Name = "Heart", MuscleType = MuscleType.Heart, BodyPartType = BodyPartType.Heart, BodySectionType = BodySectionType.Upper, MovementType = MovementType.Cardio };

            //arms x3
            MuscleDAL biceps = new() { Name = "Biceps", BodyPartType = BodyPartType.Arm, BodySectionType = BodySectionType.Upper, MovementType = MovementType.Pull, MuscleType = MuscleType.Biceps };
            MuscleDAL triceps = new() { Name = "Triceps", MuscleType = MuscleType.Triceps, BodyPartType = BodyPartType.Arm, BodySectionType = BodySectionType.Upper, MovementType = MovementType.Push };
            MuscleDAL forearm = new() { Name = "Forearm", MovementType = MovementType.Pull, BodySectionType = BodySectionType.Upper, BodyPartType = BodyPartType.Arm, MuscleType = MuscleType.Forearm };

            //butt and legs x6
            MuscleDAL glute = new() { Name = "Gluteal", MovementType = MovementType.Leg, BodySectionType = BodySectionType.Lower, BodyPartType = BodyPartType.Glute, MuscleType = MuscleType.Glute };
            MuscleDAL quad = new() { Name = "Quadriceps", MuscleType = MuscleType.Quad, BodyPartType = BodyPartType.Leg, BodySectionType = BodySectionType.Lower, MovementType = MovementType.Leg };
            MuscleDAL hamstring = new() { Name = "Hamstrings", MovementType = MovementType.Leg, BodySectionType = BodySectionType.Lower, BodyPartType = BodyPartType.Leg, MuscleType = MuscleType.Hamstring };
            MuscleDAL adductor = new() { Name = "Adductors", MuscleType = MuscleType.Adductor, BodyPartType = BodyPartType.Leg, BodySectionType = BodySectionType.Lower, MovementType = MovementType.Leg };
            MuscleDAL abductor = new() { Name = "Abductors", MovementType = MovementType.Leg, BodySectionType = BodySectionType.Lower, BodyPartType = BodyPartType.Leg, MuscleType = MuscleType.Abductor };
            MuscleDAL calves = new() { Name = "Calves", MuscleType = MuscleType.Calves, BodyPartType = BodyPartType.Leg, BodySectionType = BodySectionType.Lower, MovementType = MovementType.Leg };


            muscleList.Add(neck); muscleList.Add(infraspin); muscleList.Add(pecs); muscleList.Add(glute); muscleList.Add(calves);
            muscleList.Add(trap); muscleList.Add(lats); muscleList.Add(heart); muscleList.Add(quad);
            muscleList.Add(frontDelt); muscleList.Add(lowBack); muscleList.Add(biceps); muscleList.Add(hamstring);
            muscleList.Add(sideDelt); muscleList.Add(obliques); muscleList.Add(triceps); muscleList.Add(abductor);
            muscleList.Add(rearDelt); muscleList.Add(abs); muscleList.Add(forearm); muscleList.Add(adductor);
        }
       
    }
}
