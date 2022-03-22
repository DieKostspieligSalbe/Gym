// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const bodyWrapper = document.getElementById("body_wrapper")


const handleMuscleActivation = (e) => {
    const hovMuscle = e.target.parentNode
    const hovMuscleInstances = document.querySelectorAll(`[data-muscle=${hovMuscle.dataset.muscle}] path`)
    Array.from(hovMuscleInstances).forEach(muscle => muscle.classList.toggle(e.type === 'mousedown' ? 'muscle--open' : 'muscle--hover'))
}

bodyWrapper.addEventListener('mouseover', handleMuscleActivation)
bodyWrapper.addEventListener('mouseout', handleMuscleActivation)
bodyWrapper.addEventListener('mousedown', handleMuscleActivation)




$(function () {
    const bodyWrapper = document.getElementById("body_wrapper")
    
    const handleMuscleMouseDown = (e) => {
        console.log(e.target);
        $.ajax({
            type: "GET",
            url: "/home/processmuscleclick",
            data: { Id: e.target.parentNode.dataset.muscleId},
            success: function (data) {
                console.log(data);
                document.getElementById("selected_muscles").innerHTML = data
            }
        });

    }
    bodyWrapper.addEventListener('mousedown', handleMuscleMouseDown)


   /* $(".muscle-clicked").click(function () {
        event.preventDefault();
        console.log("Click-muscle class was clicked");*/



       
    });



//const handleMuscleHover = (e) => {

//    const hovMuscle = e.target.parentNode
//    const hovMuscleInstances = document.querySelectorAll(`[data-muscle=${hovMuscle.dataset.muscle}] path`)
//    Array.from(hovMuscleInstances)
//        .forEach(muscle => muscle.classList.toggle('muscle--hover'))
//}

//const handleMuscleClick = (e) => {
//    const hovMuscle = e.target.parentNode
//    console.log("clicked node is", hovMuscle)
//    const hovMuscleInstances = document.querySelectorAll(`[data-muscle=${hovMuscle.dataset.muscle}] path`)
//    console.log(hovMuscleInstances)
//    Array.from(hovMuscleInstances).forEach(muscle => muscle.classList.toggle('muscle--open'))
//}

//bodyWrapper.addEventListener('mouseover', handleMuscleHover)
//bodyWrapper.addEventListener('mouseout', handleMuscleHover)

//bodyWrapper.addEventListener('mousedown', handleMuscleClick)

