using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAL
{
    public class DbFiller //this fills the database with everything needed
    {
        GeneralContext _context;

        public DbFiller(GeneralContext context)
        {
            _context = context;
        }

        public void FillDatabase() 
        {
            //define muscles
            //shoulders,back and abdominal x12
            MuscleDAL neck = new() { Name = "Neck", BodyPartType = BodyPartType.Neck, BodySectionType = BodySectionType.Upper, MuscleType = MuscleType.Neck, MovementType = MovementType.Push };
            neck.ImageLink = "https://i.ibb.co/SKbZNQ0/neck.png";
            neck.Description = "A thick, muscular neck is common among bodybuilders and some athletes. It’s often associated with power and strength. A thick neck can lower your risk of injury, stress, and general neck pain. Because the neck is used in most sports, it’s important to keep it strong and healthy.";

            MuscleDAL trap = new() { Name = "Trapezius", MuscleType = MuscleType.Trap, MovementType= MovementType.Pull, BodyPartType = BodyPartType.Back, BodySectionType = BodySectionType.Upper };
            trap.ImageLink = "https://i.ibb.co/QdTCHHj/trapezius.png";
            trap.Description = "The trapezius muscles, or traps, sit at the top and center of your back. This three-part muscle attaches at the base of your skull and continues down to the middle of your spine. Exercises that specifically target this muscle group will add both size and definition to your back, and create a solid frame. Additionally, the traps not only look great when strong and defined, but they also improve your posture and help prevent shoulder injuries.";

            MuscleDAL frontDelt = new() { Name = "Anterior Deltoid", BodySectionType = BodySectionType.Upper, BodyPartType = BodyPartType.Shoulder, MovementType = MovementType.Push, MuscleType = MuscleType.FrontDelt };
            frontDelt.ImageLink = "https://i.ibb.co/R3C6FMT/front-Delt.png";
            frontDelt.Description = "The deltoids are some of the most important muscles in your body that you use on a day to day basis. Not only for strength, but they’re also a key factor in aesthetics. If the v-shaped torso is what you’re going for, well defined anterior (front) delts will help in not only making your waist seem slimmer but also having a wider chest. Just like having a broad chest, broad shoulders make you look strong and confident.";

            MuscleDAL sideDelt = new() { Name = "Lateral Deltoid", MovementType = MovementType.Push, BodyPartType = BodyPartType.Shoulder, BodySectionType= BodySectionType.Upper, MuscleType = MuscleType.SideDelt };
            sideDelt.ImageLink = "https://i.ibb.co/nCkNwbv/middle-Delt.png";
            sideDelt.Description = "The lateral deltoids (which is also called middle delts or side deltoids) are primarily responsible for shoulder abduction. Well-formed side delts also lead to that chiseled and sizeable look through the shoulders. Did someone say shoulder boulders?";

            MuscleDAL rearDelt = new() { Name = "Posterior Deltoid", MuscleType = MuscleType.RearDelt, BodySectionType = BodySectionType.Upper, BodyPartType = BodyPartType.Shoulder, MovementType = MovementType.Push };
            rearDelt.ImageLink = "https://i.ibb.co/6n3PSWR/rearDelt.png";
            rearDelt.Description = "The posterior deltoid, or rear delts, located on the back of your shoulders, are crucial to stable and healthy shoulders. Under-performing rear delts prevent you from gaining strength with overhead pressing and bench pressing. They also play a crucial role in your posture. Sitting in a hunched position with your shoulders rolled forward lengthens and weakens the rear delts.";

            MuscleDAL infraspin = new() { Name = "Rotator Cuff", MovementType = MovementType.Push, BodyPartType = BodyPartType.Back, BodySectionType = BodySectionType.Upper, MuscleType = MuscleType.Infraspinatus };
            infraspin.ImageLink = "https://i.ibb.co/mXPN4gG/infraspin.png";
            infraspin.Description = "The rotator cuff is a group of four muscles that stabilize the shoulder and allow it to move. Repetitive, overhead motions can wear down the rotator cuff muscles and are thus a common cause of injury. Heavy benching with poor form is just a recipe for rotator cuff disaster, and heavy bench pressing with good form still places large amounts of stress on these small muscles, that's why it is also important to give these little muscles a good workout.";

            MuscleDAL lats = new() { Name = "Latissimus Dorsi", MuscleType = MuscleType.Lats, BodySectionType = BodySectionType.Upper, BodyPartType = BodyPartType.Back, MovementType = MovementType.Pull };
            lats.ImageLink = "https://i.ibb.co/zS9Gyxs/lats.png";
            lats.Description = "You might not think of the lats as a mirror muscle as they are on your back. However, if you have a nice set of wings, you can see the lats from all angles. After all, it’s the biggest and broadest muscle of your upper body. And while a wide set of lats is impressive looking (that V-taper essential), the muscle also plays a major role in fitness and athletic performance.";

            MuscleDAL lowBack = new() { Name = "Lower back", MovementType = MovementType.Push, BodyPartType = BodyPartType.Back, BodySectionType = BodySectionType.Upper, MuscleType = MuscleType.LowBack };
            lowBack.ImageLink = "https://i.ibb.co/xJQ3Gm0/lowback.png";
            lowBack.Description = "Your lower back, while not the most glamorous group of muscles, is one of the most functional. The lumbar region of your back is the main bodyweight-bearing section of your spine. As such, the stronger your lower back is the better your posture, athletic performance, and mobility. You’ll also be less likely to suffer from lower back pain.";

            MuscleDAL obliques = new() { Name = "Obliques", MuscleType = MuscleType.Oblique, BodySectionType = BodySectionType.Upper, BodyPartType = BodyPartType.Abdominals, MovementType = MovementType.Pull };
            obliques.ImageLink = "https://i.ibb.co/Wz6F7qX/obliques.png";
            obliques.Description = "When training for a killer midsection, it’s safe to say most people either forget about or rarely change their oblique workout methods. These long muscles, when well trained and when body fat is low, frame your rectus abdominis (aka your six-pack) and give your waist a more tapered look, as well as protecting your spine.";

            MuscleDAL abs = new() { Name = "Abdominals", MovementType = MovementType.Pull, BodySectionType = BodySectionType.Upper, BodyPartType = BodyPartType.Abdominals, MuscleType = MuscleType.Abs };
            abs.ImageLink = "https://i.ibb.co/WWhNRWh/abs.png";
            abs.Description = "Abs are much more than a chiseled torso and an excuse to wear a tight t-shirt, they are also one of the most important muscles in your body and having a stronger core will help your strength in other lifts — helping your numbers on bench presses, overhead work, deadlifts and squats creep upwards and improve your recovery — while helping you maintain good posture and even eliminate back pain.";

            MuscleDAL pecs = new() { Name = "Pectoralis", MovementType = MovementType.Push, BodySectionType = BodySectionType.Upper, BodyPartType = BodyPartType.Chest, MuscleType = MuscleType.Pectoral };
            pecs.ImageLink = "https://i.ibb.co/Yh4YWSh/pecs.png";
            pecs.Description = @"The chest muscles could be considered a defining part of strength anatomy. They are involved in actions such as squeezing a set of loppers to cut a tree branch and pushing a door open. They are also the primary muscles referenced when debating upper body strength (“How much can you bench, bro?”). For body builders and those interested in general muscular aesthetics, the chest muscles are the defining part of muscle mass. Powerlifters rely on them for the bench press to score the greatest lift.";

            MuscleDAL heart = new() { Name = "Heart", MuscleType = MuscleType.Heart, BodyPartType = BodyPartType.Heart, BodySectionType = BodySectionType.Upper, MovementType = MovementType.Cardio };
            heart.ImageLink = "https://i.ibb.co/zVFzF8H/heart.png";
            heart.Description = "Aerobic exercise, also known as “cardio” exercise, uses repetitive contraction of large muscle groups to get your heart beating faster and is the most beneficial type of exercise for your cardiovascular system (your heart and blood vessels). It can improve the flow of oxygen throughout your body, lower your blood pressure and cholesterol, reduce your risk for heart disease, diabetes, Alzheimer’s disease, stroke, and some kinds of cancer";


            //arms x3
            MuscleDAL biceps = new() { Name = "Biceps", BodyPartType = BodyPartType.Arm, BodySectionType = BodySectionType.Upper, MovementType = MovementType.Pull, MuscleType = MuscleType.Biceps };
            biceps.ImageLink = "https://i.ibb.co/cw24BHk/biceps.png";
            biceps.Description = "Your biceps are, essentially, the large muscle group that sits on the front section of your upper arm. You know the one we’re talking about. You stare at it often enough when flexing. Not only do large, toned biceps look great, but the bicep muscles are also responsible for a considerable amount of arm function and movement.";

            MuscleDAL triceps = new() { Name = "Triceps", MuscleType = MuscleType.Triceps, BodyPartType = BodyPartType.Arm, BodySectionType = BodySectionType.Upper, MovementType = MovementType.Push };
            triceps.ImageLink = "https://i.ibb.co/THrhs00/triceps.png";
            triceps.Description = "Strong arms are important for almost every upper body movement you do each day and your triceps are often the heavy lifters. Anytime you push something—whether it be a door, a stroller, a lawnmower, or a barbell — you're using your triceps. The triceps make up roughly two-thirds of your upper arm, so if you want bigger arms, building your tris is a must.";

            MuscleDAL forearm = new() { Name = "Forearm", MovementType = MovementType.Pull, BodySectionType = BodySectionType.Upper, BodyPartType = BodyPartType.Arm, MuscleType = MuscleType.Forearm };
            forearm.ImageLink = "https://i.ibb.co/YbX63hV/forearm.png";
            forearm.Description = "When you’re training grip intensive exercises like chin-ups, deadlifts, bent-over rows, or carry variations, you will often feel your forearms burning. It’s usually the first muscle group to fatigue, and so grip strength is often a weak point for many lifters. Of course, you can throw a bandage on the issue and wear lifting straps (which are a great tool), but you should also focus on building forearm strength. But aside from helping with everyday lifting tasks, your forearm muscles play an important role in your overall appearance.";

            //butt and legs x6
            MuscleDAL glute = new() { Name = "Gluteal", MovementType = MovementType.Leg, BodySectionType = BodySectionType.Lower, BodyPartType = BodyPartType.Glute, MuscleType = MuscleType.Glute };
            glute.ImageLink = "https://i.ibb.co/PxBgfMN/glute.png";
            glute.Description = "The gluteus maximus — that is your butt for the uninitiated — is the largest muscle in the body. Even if filling out a pair of Wranglers isn’t at the top of your training priorities list, developing strong glutes will help you build better squats, deadlifts, and everything in between. If you really want to boost your lifts, include at least some glute-specific training";

            MuscleDAL quad = new() { Name = "Quadriceps", MuscleType = MuscleType.Quad, BodyPartType = BodyPartType.Leg, BodySectionType = BodySectionType.Lower, MovementType = MovementType.Leg };
            quad.ImageLink = "https://i.ibb.co/C1q3RnQ/quad.png";
            quad.Description = "If you were to ask an athlete about what muscle group looks the most impressive on the body, there’s a good chance they’d say, “the quadriceps.” Few muscles exemplify power and strength more than a pair of thick, strong quads peeking through a pair of shorts or pants. Whether you’re a recreational lifter, strength athlete, or play a sport, strong quads are essential for performance and healthy movement.";

            MuscleDAL hamstring = new() { Name = "Hamstrings", MovementType = MovementType.Leg, BodySectionType = BodySectionType.Lower, BodyPartType = BodyPartType.Leg, MuscleType = MuscleType.Hamstring };
            hamstring.ImageLink = "https://i.ibb.co/tb0bLWv/hamstrings.png";
            hamstring.Description = "If you have weak hamstrings, then there’s a strong chance you’re not maxing out on your strength potential for squats and deadlifts. Plus, lack of hamstring eccentric strength is a known cause of hamstrings strains. Paying attention to them is great for performance, aesthetics, and injury prevention.";

            MuscleDAL adductor = new() { Name = "Adductors", MuscleType = MuscleType.Adductor, BodyPartType = BodyPartType.Leg, BodySectionType = BodySectionType.Lower, MovementType = MovementType.Leg };
            adductor.ImageLink = "https://i.ibb.co/WPYZrXW/adductors.png";
            adductor.Description = "When it comes to building an impressive lower body, you tend to focus on the quads, hamstrings, and glute muscles. But certain muscles remain out of sight and mind even though they have a direct effect on your lower-body muscles’ performance. Enter the adductors.";

            MuscleDAL abductor = new() { Name = "Abductors", MovementType = MovementType.Leg, BodySectionType = BodySectionType.Lower, BodyPartType = BodyPartType.Leg, MuscleType = MuscleType.Abductor };
            abductor.ImageLink = "https://i.ibb.co/xSrYy7c/abductors.png";
            abductor.Description = "Anytime your leg moves away from the midline of your body, it’s the work of your hip abductor muscles. When working out your glutes and your lower body, it is important to place focus on this often forgotten muscle group. Through hip abductor exercises, you will get a tighter, more toned backside, stronger hips, and even stronger, more stable knees.";

            MuscleDAL calves = new() { Name = "Calves", MuscleType = MuscleType.Calves, BodyPartType = BodyPartType.Leg, BodySectionType = BodySectionType.Lower, MovementType = MovementType.Leg };
            calves.ImageLink = "https://i.ibb.co/5kKWhxZ/calves.png";
            calves.Description = "Calf exercises are often looked upon as the vainest of workout routines. It seems all they do is spruce up a muscle for seemingly cosmetic purposes. But in reality, there’s more to quality calf muscle exercises than first meets the eye. Calves don’t just indicate strong legs but are directly correlated to lower amounts of plaque build-up in the arteries (allegedly), along with better resting heart rates and more skeletal muscle. That’s reason enough for us.";


            //add muscles to db
            _context.Muscles.AddRange(neck, trap, frontDelt, sideDelt, rearDelt, infraspin, lats, lowBack, obliques, abs, pecs, heart);
            _context.Muscles.AddRange(biceps, triceps, forearm, glute, quad, hamstring, adductor, abductor, calves);
            _context.SaveChanges();




            //define equip
            EquipDAL barbell = new() { Name = "Barbell", EquipmentType = EquipType.ActiveItem};
            EquipDAL dumbbell = new() { Name = "Dumbbell", EquipmentType = EquipType.ActiveItem};
            EquipDAL kettlebell = new() { Name = "Kettlebell", EquipmentType = EquipType.ActiveItem }; //consider if it is needed
            EquipDAL bench = new() { Name = "Bench", EquipmentType = EquipType.StationaryItem };
            EquipDAL romanBench = new() { Name = "Roman Bench", EquipmentType = EquipType.StationaryItem };
            EquipDAL landmine = new() { Name = "Landmine", EquipmentType = EquipType.Machine };
            EquipDAL bar = new() { Name = "Bar", EquipmentType = EquipType.ActiveItem };
            EquipDAL mat = new() { Name = "Mat", EquipmentType = EquipType.StationaryItem };
            EquipDAL ball = new() { Name = "Ball", EquipmentType = EquipType.ActiveItem }; //consider
            EquipDAL elasticBand = new() { Name = "Elastic Band", EquipmentType = EquipType.ActiveItem }; //consider





        }

        
    }
}
