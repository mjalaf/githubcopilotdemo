// create the methods to call the apicaller.js


// Path: src/Web/mycopilotdemo/src/pages/peoplemanagement.js

import React, { Component } from 'react';
import { getAllPeople, getPersonById, createPerson, updatePerson, deletePerson } from '../apicaller';
import { Link } from 'react-router-dom';

class PeopleManagement extends Component {
    constructor(props) {
        super(props);
        this.state = {
            people: [],
            person: {
                id: null,
                firstName: "",
                lastName: "",
                email: "",
                age: null
            },
            submitted: false
        };
    }

    componentDidMount() {
        this.retrievePeople();
    }

    retrievePeople() {
        getAllPeople()
            .then(response => {
                this.setState({
                    people: response.data
                });
                console.log(response.data);
            })
            .catch(e => {
                console.log(e);
            });
    }

    refreshList() {
        this.retrievePeople();
        this.setState({
            person: {
                id: null,
                firstName: "",
                lastName: "",
                email: "",
                age: null
            },
            submitted: false
        });
    }

    setActivePerson(person, index) {
        this.setState({
            person: person,
            currentIndex: index
        });
    }

    handleInputChange(event) {
        const { name, value } = event.target;
        this.setState(prevState => ({
            person: {
                ...prevState.person,
                [name]: value
            }
        }));
    }

    savePerson() {
        var data = {
            firstName: this.state.person.firstName,
            lastName: this.state.person.lastName,
            email: this.state.person.email,
            age: this.state.person.age
        };

        createPerson(data)
            .then(response => {
                this.setState({
                    person: {
                        id: response.data.id,
                        firstName: response.data.firstName,
                        lastName: response.data.lastName,
                        email: response.data.email,
                        age: response.data.age
                    },
                    submitted: true
                });
                console.log(response.data);
            })
            .catch(e => {
                console.log(e);
            });
    }

    updatePerson() {
        updatePerson(
            this.state.person.id,
            this.state.person
        )
            .then(response => {
                console.log(response.data);
                this.setState({
                    submitted: true
                });
            })
            .catch(e => {
                console.log(e);
            });
    }

    deletePerson() {
        deletePerson(this.state.person.id)
            .then(response => {
                console.log(response.data);
                this.props.history.push('/people')
            })
            .catch(e => {
                console.log(e);
            });
    }
}