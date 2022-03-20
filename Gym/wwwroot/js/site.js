// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const bodyWrapper = document.getElementById("body_wrapper")


const handleMuscleHover = (e) => {

    const hovMuscle = e.target.parentNode
    const hovMuscleInstances = document.querySelectorAll(`[data-muscle=${hovMuscle.dataset.muscle}] path`)
    Array.from(hovMuscleInstances)
        .forEach(muscle => muscle.classList.toggle('muscle--hover'))
}

bodyWrapper.addEventListener('mouseover', handleMuscleHover)
bodyWrapper.addEventListener('mouseout', handleMuscleHover)