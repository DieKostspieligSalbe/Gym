// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const bodyWrapper = document.getElementById("body_wrapper")
const selectedMusclesWrapper = document.getElementById("selected_muscles")
const mainForm = document.getElementById("mainform")
const programWrapper = document.getElementById("training-program")
const saveForm = document.getElementById("save-form")
const saveResult = document.getElementById("program-save-state")

const handleMuscleActivation = (e) => {
  
    const hovMuscle = e.target.parentNode
    if(!hovMuscle.dataset.muscleId) return
    if (e.type === `mousedown`) {
        console.log(hovMuscle)
       // console.log(hovMuscle.id)
        if (hovMuscle.classList.contains(`muscle--open`)) {

            document.querySelector(`[data-remove-muscle="${hovMuscle.dataset.muscleId}"]`)?.parentNode.remove()
        } else {
            selectedMusclesWrapper.innerHTML += `<div class="selected-div"> ${hovMuscle.dataset.muscle} <a data-remove-muscle=${hovMuscle.dataset.muscleId} class="close">x</a></div>`
        }
    }

    const hovMuscleInstances = document.querySelectorAll(`[data-muscle-id="${hovMuscle.dataset.muscleId}"]`)
    Array.from(hovMuscleInstances).forEach(muscle => muscle.classList.toggle(e.type === 'mousedown' ? 'muscle--open' : 'muscle--hover'))
}

const generateTrainingDayMarkup = (trainingDay) => {
    return trainingDay.Exercises.map((ex) => {
        return `
                <li class="exercise">           
                     <div class="exercise-content">
                        <p class="exercise-name">${ex.Name}</p>
                        <p class="exercise-description">${ex.Description}</p>
                     </div>
                     <img class="exercise-image" src="${ex.ImageLink}" />
                </li>
         `
    })
}
const handleUnselectMuscle = (e) => {
    if (!e.target.dataset.removeMuscle) return
    e.target.parentNode.remove()
    const selectedMuscleInstances = document.querySelectorAll(`[data-muscle-id="${e.target.dataset.removeMuscle}"]`)
    console.log(`tgarget is`, e.target.dataset)
    console.log(``)
    Array.from(selectedMuscleInstances).forEach(muscle => muscle.classList.toggle('muscle--open'))
}

const handleSubmit = async (e) => {
    e.preventDefault()
    const intensity = document.getElementById("program-type").value
    const selectedParts = Array.from(document.getElementsByClassName("muscle--open")).map((el) => el.parentNode.dataset.muscleId || el.dataset.muscleId)
    const idList = [...new Set(selectedParts)]
    console.log(idList)
    try {
        const response = await fetch(`/home/ProcessMuscleSubmit`, {
            method: "post", body: JSON.stringify({ idList, intensity }), headers: { 'Content-Type': 'application/json' }
        })
        const data = await response.json()
        console.log('Data is', data)
        programWrapper.innerHTML = ''
        data.trainingDayList.forEach((trainingDay, i) => {
            console.log('Training day is ', trainingDay)
            const exercisesMarkup = generateTrainingDayMarkup(trainingDay)
            programWrapper.innerHTML += `
            <ul class="exercise-list">
               <p class="training-day-name">${trainingDay.DayName}</p>
               ${exercisesMarkup.join(' ')}
            </ul>
             <div class="resting-days"><p>${data.DaysBetweenMessage}</div> 
            `
        })
    } catch (err) {
        console.log(err)
    }
   
}

const handleSaveSubmit = async (e) => {
    e.preventDefault()
    const intensity = document.getElementById("program-type").value
    const selectedParts = Array.from(document.getElementsByClassName("muscle--open")).map((el) => el.parentNode.dataset.muscleId || el.dataset.muscleId)
    const idList = [...new Set(selectedParts)]
    const programName = document.getElementById("program-name").value
    console.log('Program name is', programName)
    const programCheckbox = document.getElementById("program-checkbox")
    let checkboxState = false
    if (programCheckbox.checked) {
        checkboxState = true
    } else { checkboxState = false }
    console.log(checkboxState)
    var model = new Object();
    model.IdList = idList
    model.Intensity = intensity
    model.Name = programName
    model.IsPublic = checkboxState
    try {
        const response = await fetch(`/home/SaveProgram`, { //add descr
            method: "post", body: JSON.stringify(model), headers: { 'Content-Type': 'application/json'}
        })
        const data = await response.json()
        console.log('Data is', data)
        let saveResult = ""
        if (data === true) {
            saveResult = "was saved"
        }
        else {saveResult = "didn't manage to save :("}
        saveResult.innerHTML += `
        <p class="save-result-label">Your program ${programName} ${saveResult}</p>
        `
    } catch (err) {
        console.log(err)
    }
}

bodyWrapper.addEventListener('mouseover', handleMuscleActivation)
bodyWrapper.addEventListener('mouseout', handleMuscleActivation)
bodyWrapper.addEventListener('mousedown', handleMuscleActivation)
mainForm.addEventListener('submit', handleSubmit)
selectedMusclesWrapper.addEventListener('mousedown', handleUnselectMuscle)
saveForm.addEventListener('submit', handleSaveSubmit)


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

