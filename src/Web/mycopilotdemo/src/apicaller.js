//Create the API call to the backend to point to PeopleManagerController
//This is the API call to the backend to point to PeopleManagerController

import axios from 'axios';

const api = axios.create({
    baseURL: "https://apim-poc-dev-eastus2-001.azure-api.net/api",
    headers: {
        "Content-type": "application/json",
        "PeopleManagement-Key": "f1272ab646884ac3b2ea31a40e7ee236"
    }
});

export default api;

// create the methos to call the backend

export const getAllPeople = () => {
    return api.get("/People");
}

export const getPersonById = id => {
    return api.get(`/People/${id}`);
}

export const createPerson = data => {
    return api.post("/People", data);
}

export const updatePerson = (id, data) => {
    return api.put(`/People/${id}`, data);
}

export const deletePerson = id => {
    return api.delete(`/People/${id}`);
}

