﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const bodyWrapper = document.getElementById("body_wrapper")
const selectedMusclesWrapper = document.getElementById("selected_muscles")

const handleMuscleActivation = (e) => {
  
    const hovMuscle = e.target.parentNode
    if(!hovMuscle.dataset.muscleId) return
    if (e.type === `mousedown`) {
        console.log(hovMuscle.id)
        if (hovMuscle.classList.contains(`muscle--open`)) {

            document.querySelector(`[data-remove-muscle="${hovMuscle.dataset.muscleId}"]`)?.parentNode.remove()
        } else {
            selectedMusclesWrapper.innerHTML += `<div class="selected-div"> ${hovMuscle.dataset.muscle} <a data-remove-muscle=${hovMuscle.dataset.muscleId} class="close">x</a></div>`
        }
    }

    const hovMuscleInstances = document.querySelectorAll(`[data-muscle-id="${hovMuscle.dataset.muscleId}"]`)
    e.type === 'mousedown' && console.log(Array.from(hovMuscleInstances))
    Array.from(hovMuscleInstances).forEach(muscle => muscle.classList.toggle(e.type === 'mousedown' ? 'muscle--open' : 'muscle--hover'))
}

const handleUnselectMuscle = (e) => {
    if (!e.target.dataset.removeMuscle) return
    e.target.parentNode.remove()
    const selectedMuscleInstances = document.querySelectorAll(`[data-muscle-id="${e.target.dataset.removeMuscle}"]`)
    console.log(`tgarget is`, e.target.dataset)
    console.log(``)
    Array.from(selectedMuscleInstances).forEach(muscle => muscle.classList.toggle('muscle--open'))
}


bodyWrapper.addEventListener('mouseover', handleMuscleActivation)
bodyWrapper.addEventListener('mouseout', handleMuscleActivation)
bodyWrapper.addEventListener('mousedown', handleMuscleActivation)
selectedMusclesWrapper.addEventListener('mousedown', handleUnselectMuscle)

const handleSubmit = () => {
    const selectedParts = Array.from(document.getElementsByClassName("muscle--open")).map((el) => el.parentNode.dataset.muscleId)
    const selectedMuscleIds = [...new Set(selectedParts)]
    console.log(selectedMuscleIds)
}


//$(function () {
//    const bodyWrapper = document.getElementById("body_wrapper")

//    const handleMuscleMouseDown = (e) => {
//        console.log(e.target);
//        handleSubmit()
//        $.ajax({
//            type: "GET",
//            url: "home/ProcessMuscleSubmit",
//            data: { selectedMuscleIds: selectedMuscleIds },
//            success: function (data) {
//                console.log(data);
//            }
//        });

//    }
//    bodyWrapper.addEventListener('mousedown', handleMuscleMouseDown)
//});



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

