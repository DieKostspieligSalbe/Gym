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
            EquipDAL barbell = new() { Name = "Barbell", EquipmentType = EquipType.ActiveItem, IsEssential = true};
            barbell.ImageLink = "https://i.ibb.co/B3qy0Wg/barbell-true.png";
            barbell.Description = "A barbell is a long metal bar to which discs of varying weights are attached at each end, used for weightlifting.";

            EquipDAL weightPlate = new() { Name = "Weight Disc", EquipmentType = EquipType.ActiveItem, IsEssential = false};
            weightPlate.ImageLink = "https://i.ibb.co/175Tk4g/barbell.jpg";
            weightPlate.Description = "Weight plates are a type of weight lifting equipment used to add resistance to adjustable barbells and dumbbells, as well as plate-loaded weight machines.";

            EquipDAL dumbbell = new() { Name = "Dumbbell", EquipmentType = EquipType.ActiveItem, IsEssential = true};
            dumbbell.ImageLink = "https://i.ibb.co/ScHCyrw/dumbbell.png";
            dumbbell.Description = "A dumbbell is a short bar with a weight at each end, used typically in pairs for exercise or muscle-building.";

            EquipDAL kettlebell = new() { Name = "Kettlebell", EquipmentType = EquipType.ActiveItem, IsEssential = false}; //consider if it is needed
            kettlebell.ImageLink = "https://i.ibb.co/wh0P14K/Kettlebell.png";
            kettlebell.Description = "A kettlebell is a type of dumbbell or free weight that is round with a flat base and an arced handle. Unlike dumbbells, only one kettlebell is usually used for exercise.";

            EquipDAL bench = new() { Name = "Bench", EquipmentType = EquipType.StationaryItem, IsEssential = true};
            bench.ImageLink = "https://i.ibb.co/BzGCVMK/bench.png";
            bench.Description = "Weight benches are versatile pieces of workout equipment that can support a wide array of workouts.";

            EquipDAL romanBench = new() { Name = "Roman Chair", EquipmentType = EquipType.StationaryItem, IsEssential = false};
            romanBench.ImageLink = "https://i.ibb.co/M733My9/roman-bench.png";
            romanBench.Description = "The Roman chair is a piece of fitness equipment primarily designed to build endurance in your lower back.";

            EquipDAL landmine = new() { Name = "Landmine", EquipmentType = EquipType.Machine, IsEssential = false};
            landmine.ImageLink = "https://i.ibb.co/YDF450f/landmine.png";
            landmine.Description = "A landmine is a barbell anchored to the floor with a weight on the other end. The angle of the bar allows you to apply force vertically and horizontally.";

            EquipDAL bar = new() { Name = "Bar", EquipmentType = EquipType.ActiveItem, IsEssential = false};
            bar.ImageLink = "https://i.ibb.co/X2LdJnB/bar.png";
            bar.Description = "A bar is a rod of various weights and shapes, can be used by itself as well as with weight plates attached.";

            EquipDAL mat = new() { Name = "Mat", EquipmentType = EquipType.StationaryItem, IsEssential = false};
            mat.ImageLink = "https://i.ibb.co/JFMSykg/mat.png";
            mat.Description = "Gym mats protect the subfloor, provide safe footing and protection from impact injury.";

            EquipDAL ball = new() { Name = "Ball", EquipmentType = EquipType.ActiveItem, IsEssential = false}; //consider
            ball.ImageLink = "https://i.ibb.co/m6QP80z/ball.png";
            ball.Description = "Exercise balls - also known as physioballs, Swiss balls, or fit balls -- are large, vinyl balls you can use to strengthen and stretch your body, improving core stability and balance.";

            EquipDAL elasticBand = new() { Name = "Resistance Band", EquipmentType = EquipType.ActiveItem, IsEssential = false}; //consider
            elasticBand.ImageLink = "https://i.ibb.co/LnpGGkZ/rubber-band.png";
            elasticBand.Description = "Resistance bands work by adding an external resistance force that can be applied without having to hold extra weight.";

            //cardio
            EquipDAL treadmill = new() { Name = "Treadmill", EquipmentType = EquipType.Machine, IsEssential = true};
            treadmill.ImageLink = "https://i.ibb.co/gRzk7CV/treadmill.png";
            treadmill.Description = "A treadmill is a device generally used for walking, running, or climbing while staying in the same place.";

            EquipDAL bike = new() { Name = "Stationary Bicycle", EquipmentType = EquipType.Machine, IsEssential = false};
            bike.ImageLink = "https://i.ibb.co/QFHQxqh/bicycle.png";
            bike.Description = "A stationary bicycle is a device used as exercise equipment for indoor cycling.";

            EquipDAL ellipse = new() { Name = "Elliptical", EquipmentType = EquipType.Machine, IsEssential = true};
            ellipse.ImageLink = "https://i.ibb.co/S6J5Q8w/ellipse.png";
            ellipse.Description = "An elliptical trainer is a stationary exercise machine used to stair climb, walk, or run without causing excessive pressure to the joints.";

            EquipDAL climber = new() { Name = "Stair Machine", EquipmentType = EquipType.Machine, IsEssential = false};
            climber.ImageLink = "https://i.ibb.co/XDf4rMv/climber.png";
            climber.Description = "Stair climber is a stationary fitness machine that rotates steps, similar to a treadmill, allowing the user to climb upward at the speed and duration they set.";

            EquipDAL rower = new() { Name = "Rower", EquipmentType = EquipType.Machine, IsEssential = true};
            rower.ImageLink = "https://i.ibb.co/s2nstvT/rower.png";
            rower.Description = "A rower, or rowing machine, is a machine used to simulate the action of watercraft rowing.";


            //machines
            EquipDAL deltMachine = new() { Name = "Delt Machine", EquipmentType = EquipType.Machine, IsEssential = false};
            deltMachine.ImageLink = "https://i.ibb.co/QYhj2Jt/delt-machine.png";
            deltMachine.Description = "The machine is used to work deltoids, primarily side delts.";

            EquipDAL pecFly = new() { Name = "Pectoral Fly/Rear Delt Machine", EquipmentType = EquipType.Machine, IsEssential = true};
            pecFly.ImageLink = "https://i.ibb.co/kG4yxzt/pec-fly.png";
            pecFly.Description = "The machine is used to work pectoral muscles and rear deltoid.";

            EquipDAL chestPress = new() { Name = "Chest Press Machine", EquipmentType = EquipType.Machine, IsEssential = false};
            chestPress.ImageLink = "https://i.ibb.co/wgjg3Wc/chest-press.png";
            chestPress.Description = "The machine is used to work pectoral muscles and triceps.";

            EquipDAL shoulderMachine = new() { Name = "Shoulder Press Machine", EquipmentType = EquipType.Machine, IsEssential = false};
            shoulderMachine.ImageLink = "https://i.ibb.co/25q6hDz/shoulder-press.png";
            shoulderMachine.Description = "The machine is used to work shoulder muscles and triceps.";

            EquipDAL crossover = new() { Name = "Crossover", EquipmentType = EquipType.Machine, IsEssential = true};
            crossover.ImageLink = "https://i.ibb.co/1QnmL8t/crossover.png";
            crossover.Description = "The crossover machine allows users to access a near-unlimited number of high and low pulley exercises while working every major muscle group. It uses cables to provide resistance, allows you to change handles or adjust height for various movements.";

            EquipDAL squatRack = new() { Name = "Squat Rack", EquipmentType = EquipType.StationaryItem, IsEssential = true};
            squatRack.ImageLink = "https://i.ibb.co/GTMcZWj/squat-rack.png";
            squatRack.Description = "A squat rack is used to support weight while doing squats, also works as a spotter.";

            EquipDAL smith = new() { Name = "Smith Machine", EquipmentType = EquipType.StationaryItem, IsEssential = true};
            smith.ImageLink = "https://i.ibb.co/7Rr9ph9/smith.png";
            smith.Description = "A Smith machine is a construction which has a barbell attached to rails. It allows the Smith machine to spot you while you exercise, but severely changes your movement trajectory, also hepling to isolate worked muscles more.";

            EquipDAL pulldown = new() { Name = "Pulldown Machine", EquipmentType = EquipType.Machine, IsEssential = true};
            pulldown.ImageLink = "https://i.ibb.co/Db2M6bV/pulldown.png";
            pulldown.Description = "The machine is used to work mainly lats and biceps, but also other muscles of back and arms.";

            EquipDAL row = new() { Name = "Seated Low Row Machine", EquipmentType = EquipType.Machine, IsEssential = true};
            row.ImageLink = "https://i.ibb.co/rHMWqsK/cable-row.png";
            row.Description = "The machine is used to work mainly lats and biceps, but also other muscles of back and arms.";

            EquipDAL chinUp = new() { Name = "Assisted Pull-Up/Dip Machine", EquipmentType = EquipType.Machine, IsEssential = true};
            chinUp.ImageLink = "https://i.ibb.co/fxf3rm0/chin-up.png";
            chinUp.Description = "This machine can help you with doing pull-ups, as well as triceps dips. It works your arm and back muscles.";

            EquipDAL seatedDip = new() { Name = "Seated Dip Machine", EquipmentType = EquipType.Machine, IsEssential = false};
            seatedDip.ImageLink = "https://i.ibb.co/fMgTQPn/seated-dip.png";
            seatedDip.Description = "This machine is used to work triceps.";


            EquipDAL obliqMachine = new() { Name = "Oblique Twist Machine", EquipmentType = EquipType.Machine, IsEssential = false };
            obliqMachine.ImageLink = "https://i.ibb.co/fMcfKw6/oblique-twist.png";
            obliqMachine.Description = "This machine is used to work oblique and abdominal muscles.";

            EquipDAL backMachine = new() { Name = "Lower Back Machine", EquipmentType = EquipType.Machine, IsEssential = false };
            backMachine.ImageLink = "https://i.ibb.co/DMfhcWt/lower-back-machine.png";
            backMachine.Description = "This machine is used to work lower back muscles.";

            EquipDAL legRaise = new() { Name = "Captain's Chair", EquipmentType = EquipType.StationaryItem, IsEssential = false };
            legRaise.ImageLink = "https://i.ibb.co/SDkS2ZV/dip-leg-raise.png";
            legRaise.Description = "This construction is used to work abdominal muscles.";

            EquipDAL absMachine = new() { Name = "Ab Crunch Machine", EquipmentType = EquipType.Machine, IsEssential = false };
            absMachine.ImageLink = "https://i.ibb.co/cxDCyZz/abs.png";
            absMachine.Description = "This machine is used to work your abdominal muscles.";

            EquipDAL preacherMachine = new() { Name = "Preacher Curl Machine", EquipmentType = EquipType.Machine, IsEssential = false };
            preacherMachine.ImageLink = "https://i.ibb.co/4tMpp9j/preacher-machine.png";
            preacherMachine.Description = "This machine is used to work your biceps.";

            EquipDAL preacherStand = new() { Name = "Preacher Curl Bench", EquipmentType = EquipType.StationaryItem, IsEssential = false };
            preacherStand.ImageLink = "https://i.ibb.co/mhD5hPn/preacher-bench.png";
            preacherStand.Description = "This item is used to work your biceps with a barbell.";

            EquipDAL hack = new() { Name = "Hack Squat Machine", EquipmentType = EquipType.Machine, IsEssential = false};
            hack.ImageLink = "https://i.ibb.co/rkV28Jc/hack-squat.png";
            hack.Description = "This machine assists you with squatting by improving your core support and targeting your quadriceps more.";

            EquipDAL legPress = new() { Name = "Leg Press Machine", EquipmentType = EquipType.Machine, IsEssential = false };
            legPress.ImageLink = "https://i.ibb.co/gZyjCFh/leg-press.png";
            legPress.Description = "This machine works your legs, primarily quads, but you can choose to also involve other leg muscles by simply adjusting your feet position.";


            EquipDAL gluteMachine = new() { Name = "Glute Machine", EquipmentType = EquipType.Machine, IsEssential = false };
            gluteMachine.ImageLink = "https://i.ibb.co/vHKQxWB/glute-machine.png";
            gluteMachine.Description = "This machine is used to work your glutes.";

            EquipDAL legExt = new() { Name = "Leg Extension Machine", EquipmentType = EquipType.Machine, IsEssential = false };
            legExt.ImageLink = "https://i.ibb.co/kgHTMYP/leg-extension.png";
            legExt.Description = "This machine is used to work your quadriceps.";

            EquipDAL legCurl = new() { Name = "Leg Curl Machine", EquipmentType = EquipType.Machine, IsEssential = false };
            legCurl.ImageLink = "https://i.ibb.co/6X4bLKY/leg-curl-true.png";
            legCurl.Description = "This machine is used to works your hamstrings.";

            EquipDAL calveMachine = new() { Name = "Calf Machine", EquipmentType = EquipType.Machine, IsEssential = false };
            calveMachine.ImageLink = "https://i.ibb.co/C61DJRq/calf-machine.png";
            calveMachine.Description = "This machine is used to work your calves.";

            EquipDAL hipThrust = new() { Name = "Hip Thrust Machine", EquipmentType = EquipType.Machine, IsEssential = false };
            hipThrust.ImageLink = "https://i.ibb.co/k2Btr2p/hip-thrust.png";
            hipThrust.Description = "This machine is used to work your glutes.";

            EquipDAL totalHip = new() { Name = "Total Hip Machine", EquipmentType = EquipType.Machine, IsEssential = false };
            totalHip.ImageLink = "https://i.ibb.co/kMmdzpH/total-hip.png";
            totalHip.Description = "This machine is used to work your hip muscles, including glutes.";

            EquipDAL abdAdd = new() { Name = "Abductors/Adductors Machine", EquipmentType = EquipType.Machine, IsEssential = false };
            abdAdd.ImageLink = "https://i.ibb.co/9q7ffS7/abd-add.png";
            abdAdd.Description = "This machine is used to work your abductors and adductors.";

            EquipDAL kickbackMachine = new() { Name = "Kickback Machine", EquipmentType = EquipType.Machine, IsEssential = false };
            kickbackMachine.ImageLink = "https://i.ibb.co/n37B8DZ/kickback.png";
            kickbackMachine.Description = "This machine is used to work your glutes and hamstrings.";

            EquipDAL benchPressItem = new() { Name = "Bench Press", EquipmentType = EquipType.StationaryItem, IsEssential = true };
            benchPressItem.ImageLink = "https://i.ibb.co/9YzZDvG/bench-press.png";
            benchPressItem.Description = "Bench press stand is used to work your pectoral muscles and triceps.";

            EquipDAL tricepsMachine = new() { Name = "Triceps Machine", EquipmentType = EquipType.Machine, IsEssential = false };
            tricepsMachine.ImageLink = "https://i.ibb.co/dbGBwcs/triceps-machine.png";
            tricepsMachine.Description = "This machine is used to work your triceps.";






            //exercises














        }


    }
}
