import 'object-assign';
import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import EmployeeService, { IEmployee, IAddEmployee, IInitAddEmployee } from '../../services/Employees';
import { RoutePaths } from '../Routes';
import { IRestResponse } from '../../services/RestUtilities';

let employeeService = new EmployeeService();

export class EmployeeForm extends React.Component<RouteComponentProps<any>, any> {
    state = {
        employee: null as IEmployee,
        addEmployee: null as IAddEmployee,
        models: null as IInitAddEmployee,
        errors: {} as { [key: string]: string },
        isAddMode: false as boolean
    }

    componentDidMount() {
        employeeService.addInit().then((response) => {
            this.setState({ models: response.content });
            if (this.props.match.path == RoutePaths.EmployeeEdit) {
                employeeService.fetch(this.props.match.params.id).then((response) => {
                    this.setState({ employee: response.content, isAddMode: false });
                });
            } else {
                let newEmployee: IAddEmployee = {
                    lastName: '', firstName: '', officialEmail: '', email: '',
                    mobilePhone: '', phone: '',
                    employeeTypeId: this.state.models.types[0].id,
                    designationId: this.state.models.designations[0].id,
                    employeeStatusId: this.state.models.statuses[0].id,
                    abaAccountName: '', abaAccountNumber: ''
                };
                this.setState({ employee: newEmployee, isAddMode: true });
            }
        });
    }

    handleSubmit(event: React.FormEvent<HTMLFormElement>) {
        event.preventDefault();
        this.saveEmployee(this.state.employee);
    }

    handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
        this.handleChange(event);
    }

    handleSelectChange(event: React.ChangeEvent<HTMLSelectElement>) {
        this.handleChange(event);
    }

    handleTextAreaChange(event: React.ChangeEvent<HTMLTextAreaElement>) {
        this.handleChange(event);
    }

    handleChange(event: React.ChangeEvent<any>) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;
        let employeeUpdates = {
            [name]: value
        };

        this.setState({
            employee: Object.assign(this.state.employee, employeeUpdates)
        });
    }

    saveEmployee(employee: any) {
        this.setState({ errors: {} as { [key: string]: string } });
        if (this.state.isAddMode) {
            employeeService.create(employee).then((response) => {
                this.handleResponse(response);
            });
        } else {
            employeeService.update(employee).then((response) => {
                this.handleResponse(response);
            });
        }
    }

    handleResponse(response: IRestResponse<any>) {
        if (!response.is_error) {
            this.props.history.push(RoutePaths.Employees);
        } else {
            this.setState({ errors: response.error_content });
        }
    }

    _formGroupClass(field: string) {
        var className = "form-group";
        if (field) {
            className += "has-danger";
        }
        return className;
    }

    render() {
        if (!this.state.employee) {
            return <div>Loading...</div>;
        }
        else {
            return <fieldset className="form-group">
                <legend>{this.state.employee.id ? "Edit Employee" : "New Employee"}</legend>
                <form onSubmit={(e) => this.handleSubmit(e)}>
                    <div className="row">
                        <div className="col-lg-6">
                            <div className={this._formGroupClass(this.state.errors.lastName)}>
                                <label htmlFor="inputLastName" className="form-control-label">Last Name</label>
                                <input type="text" autoFocus name="lastName" id="inputLastName" value={this.state.employee.lastName || ''}
                                    onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" required />
                                <div className="form-control-feedback">{this.state.errors.lastName}</div>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className={this._formGroupClass(this.state.errors.firstName)}>
                                <label htmlFor="inputFirstName" className="form-control-label">First Name</label>
                                <input type="text" name="firstName" id="inputFirstName" value={this.state.employee.firstName || ''}
                                    onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" required />
                                <div className="form-control-feedback">{this.state.errors.firstName}</div>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-lg-4">
                            <div className={this._formGroupClass(this.state.errors.status)}>
                                <label htmlFor="inputStatus" className="form-control-label">Status</label>
                                <select name="employeeStatusId" className="form-control form-control-danger" value={this.state.employee.employeeStatusId}
                                    onChange={(e) => this.handleSelectChange(e)} required>
                                    {this.state.models.statuses.map((status, index) => {
                                        return <option value={status.id} key={status.id}>{status.code}</option>
                                    })}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-4">
                            <div className={this._formGroupClass(this.state.errors.type)}>
                                <label htmlFor="inputType" className="form-control-label">Type</label>
                                <select name="employeeTypeId" className="form-control form-control-danger" value={this.state.employee.employeeTypeId}
                                    onChange={(e) => this.handleSelectChange(e)} required>
                                    {this.state.models.types.map((type, index) => {
                                        return <option value={type.id} key={type.id}>{type.name}</option>
                                    })}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-4">
                            <div className={this._formGroupClass(this.state.errors.designation)}>
                                <label htmlFor="inputDesignation" className="form-control-label">Designation</label>
                                <select name="designationId" className="form-control form-control-danger" value={this.state.employee.designationId}
                                    onChange={(e) => this.handleSelectChange(e)} required>
                                    {this.state.models.designations.map((designation, index) => {
                                        return <option value={designation.id} key={designation.id}>{designation.name}</option>
                                    })}
                                </select>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-lg-4">
                            <div className={this._formGroupClass(this.state.errors.hiredAt)}>
                                <label htmlFor="inputHiredAt" className="form-control-label">Hired At</label>
                                <input name="hiredAt" className="form-control form-control-danger" type="date"
                                    onChange={(e) => this.handleInputChange(e)} 
                                    value={this.state.employee.hiredAt || ''} />
                            </div>
                        </div>
                        <div className="col-lg-4">
                            <div className={this._formGroupClass(this.state.errors.leaveAt)}>
                                <label htmlFor="inputLeaveAt" className="form-control-label">Leave At</label>
                                <input name="leaveAt" className="form-control form-control-danger" type="date" 
                                    onChange={(e) => this.handleInputChange(e)} 
                                    value={this.state.employee.leaveAt || ''} />
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-lg-4">
                            <div className={this._formGroupClass(this.state.errors.isUnderProbation)}>
                                <label className="custom-control custom-checkbox">
                                    <input name="isUnderProbation" type="checkbox" className="custom-control-input"
                                        value={this.state.employee.isUnderProbation != null ? this.state.employee.isUnderProbation.toString() : '' || ''}
                                        checked={this.state.employee.isUnderProbation}
                                        onChange={(e) => this.handleInputChange(e)} />
                                    <span className="custom-control-indicator"></span>
                                    <span className="custom-control-description">On Probation?</span>
                                </label>
                            </div>
                        </div>
                        {!this.state.employee.isUnderProbation ? <div className="col-lg-4">
                            <div className={this._formGroupClass(this.state.errors.endedProbationAt)}>
                                <label htmlFor="inputEndedProbabtionAt" className="form-control-label">End Probation At</label>
                                <input name="endedProbationAt" className="form-control form-control-danger" type="date"
                                    onChange={(e) => this.handleInputChange(e)}
                                    value={this.state.employee.endedProbationAt || ''} />
                            </div>
                        </div> : ''}
                    </div>
                    <div className="row">
                        <div className="col-lg-6">
                            <div className={this._formGroupClass(this.state.errors.email)}>
                                <label htmlFor="inputEmail" className="form-control-label">Email</label>
                                <input type="email" name="email" id="inputEmail" value={this.state.employee.email || ''}
                                    onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" required />
                                <div className="form-control-feedback">{this.state.errors.email}</div>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className={this._formGroupClass(this.state.errors.officialEmail)}>
                                <label htmlFor="inputOfficialEmail" className="form-control-label">Official Email</label>
                                <input type="email" name="officialEmail" id="inputOfficialEmail" value={this.state.employee.officialEmail || ''}
                                    onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" />
                                <div className="form-control-feedback">{this.state.errors.officialEmail}</div>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-lg-6">
                            <div className={this._formGroupClass(this.state.errors.phone)}>
                                <label htmlFor="inputPhone" className="form-control-label">Phone</label>
                                <input type="tel" name="phone" id="inputPhone" value={this.state.employee.phone || ''}
                                    onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" />
                                <div className="form-control-feedback">{this.state.errors.phone}</div>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className={this._formGroupClass(this.state.errors.mobilePhone)}>
                                <label htmlFor="inputMobilePhone" className="form-control-label">Mobile Phone</label>
                                <input type="tel" name="mobilePhone" id="inputMobilePhone" value={this.state.employee.mobilePhone || ''}
                                    onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" />
                                <div className="form-control-feedback">{this.state.errors.mobilePhone}</div>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-lg-12">
                            <div className={this._formGroupClass(this.state.errors.street)}>
                                <label htmlFor="inputStreet" className="form-control-label">Street</label>
                                <input type="text" name="street" id="inputStreet" value={this.state.employee.street || ''}
                                    onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" />
                                <div className="form-control-feedback">{this.state.errors.street}</div>
                            </div>

                        </div>
                    </div>
                    <div className="row">
                        <div className="col-lg-6">
                            <div className={this._formGroupClass(this.state.errors.state)}>
                                <label htmlFor="inputState" className="form-control-label">State</label>
                                <input type="text" name="state" id="inputState" value={this.state.employee.state || ''}
                                    onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" />
                                <div className="form-control-feedback">{this.state.errors.state}</div>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className={this._formGroupClass(this.state.errors.city)}>
                                <label htmlFor="inputCity" className="form-control-label">City</label>
                                <input type="text" name="city" id="inputCity" value={this.state.employee.city || ''}
                                    onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" />
                                <div className="form-control-feedback">{this.state.errors.city}</div>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-lg-6">
                            <div className={this._formGroupClass(this.state.errors.zipCode)}>
                                <label htmlFor="inputZipCode" className="form-control-label">Zip Code</label>
                                <input type="text" name="zipCode" id="inputZipCode" value={this.state.employee.zipCode || ''} onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" />
                                <div className="form-control-feedback">{this.state.errors.zipCode}</div>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className={this._formGroupClass(this.state.errors.country)}>
                                <label htmlFor="inputCountry" className="form-control-label">Country</label>
                                <input type="text" name="country" id="inputCountry" value={this.state.employee.country || ''} onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" />
                                <div className="form-control-feedback">{this.state.errors.country}</div>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-lg-6">
                            <div className={this._formGroupClass(this.state.errors.abaAccountName)}>
                                <label htmlFor="inputAbaAccountName" className="form-control-label">ABA Account Name</label>
                                <input type="text" name="abaAccountName" id="inputAbaAccountName" value={this.state.employee.abaAccountName || ''} onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" />
                                <div className="form-control-feedback">{this.state.errors.abaAccountName}</div>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className={this._formGroupClass(this.state.errors.abaAccountNumber)}>
                                <label htmlFor="inputAbaAccountNumber" className="form-control-label">ABA Account Number</label>
                                <input type="text" name="abaAccountNumber" id="inputAbaAccountNumber" value={this.state.employee.abaAccountNumber || ''} onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" />
                                <div className="form-control-feedback">{this.state.errors.abaAccountNumber}</div>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-lg-12">
                            <div className={this._formGroupClass(this.state.errors.phone)}>
                                <label htmlFor="inputRemarks" className="form-control-label">Remarks</label>
                                <textarea name="remarks" id="remarks" value={this.state.employee.remarks || ''} onChange={(e) => this.handleTextAreaChange(e)} className="form-control form-control-danger" />
                                <div className="form-control-feedback">{this.state.errors.remarks}</div>
                            </div>
                        </div>
                    </div>
                    <button className="btn btn-md btn-primary" type="submit">Save</button>
                    <Link className="btn btn-md btn-secondary" to="/employees">Cancel</Link>
                </form>
            </fieldset>
        }
    }
}