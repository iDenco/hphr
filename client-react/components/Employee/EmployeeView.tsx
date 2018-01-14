import 'object-assign';
import * as React from 'react';
import * as classnames from 'classnames';
import { RoutePaths } from '../Routes';
import { Link, RouteComponentProps } from 'react-router-dom';
import EmployeeService, { IEmployee } from '../../services/Employees';
import { IRestResponse } from '../../services/RestUtilities';

let employeeService = new EmployeeService();

export class EmployeeView extends React.Component<RouteComponentProps<any>, any> {

    state = {
        employee: null as IEmployee,
        errors: {} as { [key: string]: string }
    }

    componentDidMount() {
        employeeService.fetch(this.props.match.params.id).then((response) => {
            this.setState({ employee: response.content });
            console.log(response.content);
        });
    }
    render() {
        if (!this.state.employee) {
            return <div>Loading...</div>
        }
        else {
            return <div>
                <div className="row">
                    <div className="col-lg-12">
                        <span className="color-light-red display-4">Information</span>
                        <Link to={RoutePaths.EmployeeEdit.replace(":id", this.state.employee.id.toString())} className="lead color-light-blue float-right">edit</Link>
                    </div>
                </div>
                <div className="card-group">
                    <div className="card card-padding">
                        <div className="card-body">
                            <h4 className="card-title color-light-blue">Basic Info</h4>
                            <hr />
                            <p className="card-text font-14">
                                <strong className="color-light-red">NAME:</strong>
                                <br />{this.state.employee.firstName} {this.state.employee.lastName || ''}
                            </p>
                            <p className="card-text font-14">
                                <strong className="color-light-red">STATUS:</strong>
                                <br /><div className={`padding-5 badge ${this.state.employee.statusColorClassName}`}>{this.state.employee.employeeStatus.toUpperCase()}</div>
                            </p>
                            <p className="card-text font-14">
                                <strong className="color-light-red">ABA ACCOUNT NAME:</strong>
                                <br />{this.state.employee.abaAccountName || '-'}
                            </p>
                            <p className="card-text font-14">
                                <strong className="color-light-red">ABA ACCOUNT NO.:</strong>
                                <br />{this.state.employee.abaAccountNumber || '-'}
                            </p>
                            <p className="card-text font-14">
                                <strong className="color-light-red">ADDRESS:</strong><br />
                                {this.state.employee.street ? this.state.employee.street + ", " : ''}
                                {this.state.employee.city ? this.state.employee.city + ", " : ''}
                                {this.state.employee.state ? this.state.employee.state + ", " : ''}
                                {this.state.employee.country ? this.state.employee.country : ''}
                            </p>
                        </div>
                    </div>
                    <div className="card card-padding">
                        <div className="card-body">
                            <h4 className="card-title color-light-blue">Employment</h4>
                            <hr />
                            <p className="card-text">This card has supporting text below as a natural lead-in to additional content.</p>
                            <p className="card-text"><small className="text-muted">Last updated 3 mins ago</small></p>
                        </div>
                    </div>
                    <div className="card card-padding">
                        <div className="card-body">
                            <h4 className="card-title color-light-blue">Contacts</h4>
                            <hr />
                            <p className="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This card has even longer content than the first to show that equal height action.</p>
                            <p className="card-text"><small className="text-muted">Last updated 3 mins ago</small></p>
                        </div>
                    </div>
                </div>

                <br />
                <div><h3 className="color-light-red display-4">Shifts</h3></div>
                <div className="card-group">
                    <div className="card card-padding">
                        <div className="card-body">
                            <h4 className="card-title color-light-blue">Basic Info</h4>
                            <hr />
                            <p className="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
                            <p className="card-text"><small className="text-muted">Last updated 3 mins ago</small></p>
                        </div>
                    </div>
                    <div className="card card-padding">
                        <div className="card-body">
                            <h4 className="card-title color-light-blue">Employment</h4>
                            <hr />
                            <p className="card-text">This card has supporting text below as a natural lead-in to additional content.</p>
                            <p className="card-text"><small className="text-muted">Last updated 3 mins ago</small></p>
                        </div>
                    </div>
                    <div className="card card-padding">
                        <div className="card-body">
                            <h4 className="card-title color-light-blue">Contacts</h4>
                            <hr />
                            <p className="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This card has even longer content than the first to show that equal height action.</p>
                            <p className="card-text"><small className="text-muted">Last updated 3 mins ago</small></p>
                        </div>
                    </div>
                </div>
                <br />
                <div><h3 className="color-light-red display-4">Leaves</h3></div>
                <div className="card-group">
                    <div className="card card-padding">
                        <div className="card-body">
                            <h4 className="card-title color-light-blue">Basic Info</h4>
                            <hr />
                            <p className="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
                            <p className="card-text"><small className="text-muted">Last updated 3 mins ago</small></p>
                        </div>
                    </div>
                    <div className="card card-padding">
                        <div className="card-body">
                            <h4 className="card-title color-light-blue">Employment</h4>
                            <hr />
                            <p className="card-text">This card has supporting text below as a natural lead-in to additional content.</p>
                            <p className="card-text"><small className="text-muted">Last updated 3 mins ago</small></p>
                        </div>
                    </div>
                    <div className="card card-padding">
                        <div className="card-body">
                            <h4 className="card-title color-light-blue">Contacts</h4>
                            <hr />
                            <p className="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This card has even longer content than the first to show that equal height action.</p>
                            <p className="card-text"><small className="text-muted">Last updated 3 mins ago</small></p>
                        </div>
                    </div>
                </div>
            </div>
        }
    }
}