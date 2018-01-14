import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Route, Redirect, Switch } from 'react-router-dom';
import { SignIn, Register } from './Auth';
import AuthService from '../services/Auth';
import { ErrorPage } from './Error';
import { Contacts } from './Contacts';
import { Employees } from './Employee/Employees';
import { ContactForm } from './ContactForm';
import { EmployeeForm } from './Employee/EmployeeForm';
import { EmployeeView } from './Employee/EmployeeView';
import { Header } from './Header';

export class RoutePaths {
    public static Contacts: string = "/contacts";
    public static ContactEdit: string = "/contacts/edit/:id";
    public static ContactNew: string = "/contacts/new";
    public static SignIn: string = "/";
    public static Register: string = "/register/";

    public static Employees: string = "/employees";
    public static EmployeeView: string = '/employees/view/:id';
    public static EmployeeEdit: string = "/employees/edit/:id";
    public static EmployeeNew: string = "/employees/new";
}

export default class Routes extends React.Component<any, any> {
    render() {
        return <Switch>
            <Route exact path={RoutePaths.SignIn} component={SignIn} />
            <Route path={RoutePaths.Register} component={Register} />

            <DefaultLayout exact path={RoutePaths.Contacts} component={Contacts} />
            <DefaultLayout path={RoutePaths.ContactNew} component={ContactForm} />
            <DefaultLayout path={RoutePaths.ContactEdit} component={ContactForm} />

            <DefaultLayout exact path={RoutePaths.Employees} component={Employees} />
            <DefaultLayout path={RoutePaths.EmployeeView} component={EmployeeView} />
            <DefaultLayout path={RoutePaths.EmployeeNew} component={EmployeeForm} />
            <DefaultLayout path={RoutePaths.EmployeeEdit} component={EmployeeForm} />
            <Route path='/error/:code?' component={ErrorPage} />
        </Switch>
    }
}

const DefaultLayout = ({ component: Component, ...rest }: { component: any, path: string, exact?: boolean }) => (
    <Route {...rest} render={props => (
        AuthService.isSignedInIn() ? (
            <div>
                <Header {...props} />
                <div className="container">
                    <Component {...props} />
                </div>
            </div>
        ) : (
                <Redirect to={{
                    pathname: RoutePaths.SignIn,
                    state: { from: props.location }
                }} />
            )
    )} />
);
