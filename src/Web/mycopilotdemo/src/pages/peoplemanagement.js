// create the methods to call the apicaller.js


// Path: src/Web/mycopilotdemo/src/pages/peoplemanagement.js

import React, { Component } from 'react';
import { getAllPeople, getPersonById, createPerson, updatePerson, deletePerson } from '../apicaller';

class PeopleManagement extends Component {
    constructor(props) {
        super(props);
        this.state = {
            people: [],
            person: {
                id: "",
                firstName: "",
                lastName: "",
                age: 0,
                address: "",
                city: "",
                state: "",
                zipCode: ""
            },
            submitted: false
        };
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
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
                id: "",
                firstName: "",
                lastName: "",
                age: 0,
                address: "",
                city: "",
                state: "",
                zipCode: ""
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
      this.setState(prevState => {
        const person = prevState.person || {};
        return {
          person: {
            ...person,
            [name]: value
          }
        };
      });
    }

    handleSubmit(event) {
      event.preventDefault();
      if (this.state.person.id) {
        updatePerson(this.state.person.id, this.state.person)
          .then(response => {
            console.log(response.data);
            this.setState({
              submitted: true
            });
            this.refreshList();
            this.retrievePeople();
          })
          .catch(e => {
            console.log(e);
          });
      } else {
        const person = this.state.person;
        delete person.id;
        createPerson(person)
          .then(response => {
            console.log(response.data);
            this.setState({
              person: {
                id: response.data.id,
                firstName: response.data.firstName,
                lastName: response.data.lastName,
                age: response.data.age,
                address: response.data.address,
                city: response.data.city,
                state: response.data.state,
                zipCode: response.data.zipCode
            },
              submitted: true
            });
            this.refreshList();
            this.retrievePeople();
          })
          .catch(e => {
            console.log(e);
          });
      }
    }

    deletePerson(id) {
        deletePerson(id)
            .then(response => {
                console.log(response.data);
                this.retrievePeople();
                this.props.history.push('/people')
            })
            .catch(e => {
                console.log(e);
            }
            );
    }

    render() {
        return (
          <div>
            <h1>People Management</h1>
            <form onSubmit={this.handleSubmit}>
              <div class="form-row">
                <div class="form-column">
                <label>
                First Name:
                <input type="text" name="firstName" value={this.state.person.firstName} onChange={this.handleInputChange} />
              </label>
              <label>
                Last Name:
                <input type="text" name="lastName" value={this.state.person.lastName} onChange={this.handleInputChange} />
              </label>
              <label>
                Age:
                <input type="number" name="age" value={this.state.person.age} onChange={this.handleInputChange} />
              </label>

                </div>
                <div class="form-column">
                <label>
                Address:
                <input type="text" name="address" value={this.state.person.address} onChange={this.handleInputChange} />
              </label>
              <label>
                City:
                <input type="text" name="city" value={this.state.person.city} onChange={this.handleInputChange} />
              </label>
              <label>
                State:
                <input type="text" name="state" value={this.state.person.state} onChange={this.handleInputChange} />
              </label>
              <label>
                Zip Code:
                <input type="text" name="zipCode" value={this.state.person.zipCode} onChange={this.handleInputChange} />
              </label>

                </div>
              </div>
              <button type="submit">Submit</button>
            </form>
            <table>
              <thead>
                <tr>
                  <th>First Name</th>
                  <th>Last Name</th>
                  <th>Age</th>
                  <th>Address</th>
                  <th>City</th>
                  <th>State</th>
                  <th>Zip Code</th>
                </tr>
              </thead>
              <tbody>
                {this.state.people.map(person => (
                  <tr key={person.id}>
                    <td>{person.firstName}</td>
                    <td>{person.lastName}</td>
                    <td>{person.age}</td>
                    <td>{person.address}</td>
                    <td>{person.city}</td>
                    <td>{person.state}</td>
                    <td>{person.zipCode}</td>
                    <td>
                      <button onClick={() => this.setState({ person: person })}>Edit</button>
                      <button onClick={() => this.deletePerson(person.id)}>Delete</button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        );
      }
}
export default PeopleManagement;