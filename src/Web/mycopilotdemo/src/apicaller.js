//Create the API call to the backend to point to PeopleManagerController
//This is the API call to the backend to point to PeopleManagerController

import axios from 'axios';

export default axios.create({
    baseURL: "http://localhost:8080/api",
    headers: {
        "Content-type": "application/json"
    }
});

// create the methos to call the backend

export const getAllPeople = () => {
    return http.get("/people");
}

export const getPersonById = id => {
    return http.get(`/people/${id}`);
}

export const createPerson = data => {
    return http.post("/people", data);
}

export const updatePerson = (id, data) => {
    return http.put(`/people/${id}`, data);
}

export const deletePerson = id => {
    return http.delete(`/people/${id}`);
}

