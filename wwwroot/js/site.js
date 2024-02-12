// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const wrapper = document.querySelector('.mw')


$(document).ready(function () {
    $('#Team').autocomplete({
        source: '/Football/Teams/search'
    });
});

const form = document.querySelector('.footballer-form')
if (form != null)
{
    form.addEventListener('submit', () => {
        const div = document.querySelector('.add-confirmation')
        div.style.display = 'block'
        div.style.animation = 'anime 3s'
        setTimeout(() => {
            div.style.display = 'none'
            div.style.animation = ''
        }, 3000)
    })
}

const openModal = (event) => {
    const country = document.querySelector("#Country")
    country.value = event.target.parentElement.previousElementSibling.textContent

    const team = document.querySelector("#Team")
    team.value = event.target.parentElement.previousElementSibling
        .previousElementSibling.textContent

    const dob = document.querySelector("#Dob")
    const dobText = event.target.parentElement.previousElementSibling
        .previousElementSibling.previousElementSibling.textContent
    const dobTextParts = dobText.split(".")
    const formattedDate = dobTextParts[2] + '-' + dobTextParts[1] + '-' + dobTextParts[0]
    dob.value = formattedDate


    const gender = event.target.parentElement.previousElementSibling
        .previousElementSibling.previousElementSibling.previousElementSibling.textContent.trim()
    if (gender === "Мужчина") {
        document.querySelector("#male").checked = true;
    } else if (gender === "Женщина") {
        document.querySelector("#female").checked = true;
    }

    const surname = document.querySelector("#Surname")
    surname.value = event.target.parentElement.previousElementSibling
        .previousElementSibling.previousElementSibling.previousElementSibling
        .previousElementSibling.textContent

    const firstname = document.querySelector("#FirstName")
    firstname.value = event.target.parentElement.previousElementSibling
        .previousElementSibling.previousElementSibling.previousElementSibling
        .previousElementSibling.previousElementSibling.textContent

    const id = document.querySelector("#Id")
    id.value = event.target.getAttribute('data-footballer-id')

    if (!wrapper) return
    wrapper.classList.add('active')
}

const buttons = document.querySelectorAll('.edit-footballer')
if (buttons.length) {
    buttons.forEach(button => {
        button.addEventListener('click', openModal)
    }) 
} 

wrapper.addEventListener('click', (event) => {
    if (event.target !== wrapper) return
    wrapper.classList.remove('active')
})