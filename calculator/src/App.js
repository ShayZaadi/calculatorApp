import axios from 'axios';
import { useEffect, useState } from 'react';
import './App.css';

const calculationsHistoryMock = []

const operators_arr = ["+", "-", "*", "/"]

function App() {

  const [calculationsHistory, setCalculationsHistory] = useState(calculationsHistoryMock)
  const [currentCalculation, setCurrentCalculation] = useState({ id:"0", input1: "", input2: "", operator: "+", result: "" })
  const [operators, setOperators] = useState(operators_arr)
  const [isEditMode, setIsEditMode] = useState(false)

  useEffect(() => {
    const script = document.createElement('script');

  script.src = "https://kit.fontawesome.com/a076d05399.js";
  script.crossorigin = "anonymous";
  script.async = true;

  document.body.appendChild(script);
    fetchData();
  }, [])

  //Fetch all data history to the table 
  const fetchData = async () => {
    console.log("start fetchData()")
    const response = await axios.get('http://localhost:19990/api/data')
    .then(function (response) {
      console.log(response.data)
      //Update the table in current data history
      setCalculationsHistory(response.data[0])
      })
      .catch(function (error) {
        console.log(error);
        alert(error);
      });
  }

  const createRequest = () => {
    console.log("start create request()");
    currentCalculation.id = "0"
    axios({
      method: "post",
      url: "http://localhost:19990/api/data",
      data: currentCalculation
    }).then(function (response) {
    console.log(response.data);
    //update the current calculation after the success of request 
    setCurrentCalculation(response.data[0])
    fetchData();
    })
    .catch(function (error) {
      console.log(error);
      alert(error);
    });
  }

  const UpdateRequest = () => {
    console.log("start UpdateRequest()");

    axios({
      method: "put",
      url: "http://localhost:19990/api/data?id=" + currentCalculation.id,
      data: currentCalculation
    }).then(function (response) {
    console.log(response.data);
    fetchData();
    setIsEditMode(false)
    currentCalculation.id = "0"
    })
    .catch(function (error) {
      console.log(error);
      alert(error)
    });
  }

  const calculateRequest = () => {
    console.log("start calculateRequest()");
    console.log(currentCalculation)
    axios({
      method: 'post',
      url: 'http://localhost:19990/api/data/calculate',
      data: currentCalculation
    }).then(function (response) {
      //update the result input after the success of request
      currentCalculation.result = response.data[0].result;
      console.log(currentCalculation);
      if(isEditMode == true){
        UpdateRequest()      }
      else{
        createRequest()
      }
    })
    .catch(function (error) {
      console.log(error);
    });
  }

  const deleteRequest = async(calculationObject) => {
    console.log("start deleteRequest()");
    console.log(calculationObject)
    await axios.delete('http://localhost:19990/api/data/' + calculationObject.id)
    .then(function (response) {
      console.log(response.data);
      fetchData();
    })
    .catch(function (error) {
      console.log(error);
      alert(error);
    });
  }

  const handleChange = (event) => {
    const { name, value } = event.target
    setCurrentCalculation({
      ...currentCalculation,
      [name]: value
    })
  }

  const handleUpdateClick = (calculationObject) => {
    setIsEditMode(true)
    setCurrentCalculation(calculationObject);
    console.log(currentCalculation)
  }

  const handleDeleteClick = (calculationObject) => {
    deleteRequest(calculationObject);
  }

  const handleSubmit = async (event) => {
    event.preventDefault()
    calculateRequest()
  }

  return (
    <div className="App">
      <h1>Calculator App</h1>

      <form onSubmit={handleSubmit}>
        <input value={currentCalculation.input1} onChange={handleChange} name="input1" />
        <select value={currentCalculation.operator} onChange={handleChange} name="operator">
          {
            operators.map((operator, index) => <option key={index}>{operator}</option>)
          }
        </select>
        <input value={currentCalculation.input2} onChange={handleChange} name="input2" />
        <input type="submit" value="=" onClick={handleSubmit} />
        <input value={currentCalculation.result} onChange={handleChange} name="result" />
      </form>
      <table>
        <thead>
          <tr>
            <th>
              Calculation
            </th>
            <th>
              Update
            </th>
            <th>
              Delete
            </th>
          </tr>
        </thead>
        <tbody>
          {
            calculationsHistory.map(calculationHistory => (
              <tr key={calculationHistory.id}>
                <td>{calculationHistory.input1} {calculationHistory.operator} {calculationHistory.input2} = {calculationHistory.result}</td>
                <td><button className="fas fa-pen" onClick={() => handleUpdateClick(calculationHistory)}></button></td>
                <td><button className='fas fa-trash' onClick={() => handleDeleteClick(calculationHistory)}></button></td>
              </tr>
            ))
          }
        </tbody>
      </table>
    </div>
  );
}

export default App;
