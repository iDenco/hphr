import * as React from 'react';
import { Link, Redirect } from 'react-router-dom';
import { RoutePaths } from '../Routes';
import EmployeeService, { IListEmployee } from '../../services/Employees';
import { RouteComponentProps } from 'react-router';

let employeeService = new EmployeeService();

export class Employees extends React.Component<RouteComponentProps<any>, any> {
    refs: {
        query: HTMLInputElement;
    }

    state = {
        employees: [] as Array<IListEmployee>,
        editEmployee: null as Object,
        isAddMode: false as boolean,
        searchQuery: '' as string
    };

    componentDidMount() {
        this.showAll();
    }

    showAll() {
        employeeService.fetchAll().then((response) => {
            this.setState({ searchQuery: '', employees: response.content });
            console.log(this.state.employees);
        })
    }

    handleSearchQueryChange(event: React.ChangeEvent<HTMLInputElement>) {
        this.setState({ searchQuery: event.target.value });
    }

    handleSearchSubmit(event: React.FormEvent<HTMLFormElement>) {
        event.preventDefault();

        if(!this.state.searchQuery){
            this.showAll();
            return;
        }

        employeeService.search(this.state.searchQuery).then((response) => {
            this.setState({ employees: response.content });
        });
    }

    delete(employee: IListEmployee) {
        employeeService.delete(employee.id).then((response) => {
            let updateEmployees = this.state.employees;
            updateEmployees.splice(updateEmployees.indexOf(employee), 1);
            this.setState({ employees: updateEmployees });
        })
    }

    render() {
        return <div>
            <h1>Employees</h1>
            <form className="form-inline my-2 my-lg-0" onSubmit={(e) => this.handleSearchSubmit(e)}>
                <input className="form-control form-control form-control-sm" type="text" value={this.state.searchQuery} onChange={(e) => this.handleSearchQueryChange(e)} placeholder="Search" />
                <button className="btn btn-outline-success btn-sm" type="submit">Search</button>&nbsp;
            </form>
            {this.state.searchQuery && this.state.employees && this.state.employees.length == 0 &&
                <p>No results!</p>
            }
            {this.state.employees && this.state.employees.length > 0 &&
                <table className="table responsive-table">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Official Email</th>
                            <th>Email</th>
                            <th>Phone</th>
                            <th>Mobile Phone</th>
                            <th>Position</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.employees.map((employee, index) =>
                            <tr key={employee.id}>
                                <td><Link to={RoutePaths.EmployeeView.replace(":id", employee.id.toString())}>{employee.lastName || ''} {employee.firstName|| ''}</Link><br /><small className={`padding-5 badge ${employee.statusColorClassName}`}>{employee.employeeStatus}</small></td>
                                <td>{employee.email || '-'}</td>
                                <td>{employee.officialEmail || '-'}</td>
                                <td>{employee.phone || '-'}</td>
                                <td>{employee.mobilePhone|| '-'}</td>
                                <td>{employee.designation}<br /><small className={`padding-5 badge ${employee.employeeTypeColorClassName}`}>{employee.employeeType}</small></td>                                
                                <td><Link to={RoutePaths.EmployeeEdit.replace(":id", employee.id.toString())}>edit</Link>
                                    <button type="button" className="btn btn-link" onClick={(e) => this.delete(employee)}>delete</button></td>
                            </tr>
                        )}
                    </tbody>
                </table>
            }
            {this.state.searchQuery &&
                <button type="button" className="btn btn-primary" onClick={(e) => this.showAll()}>clear search</button>
            }
            <Link className="btn btn-success" to={RoutePaths.EmployeeNew}>add</Link>
        </div>
    }
}