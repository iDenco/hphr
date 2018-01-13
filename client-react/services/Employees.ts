import RestUtilities from './RestUtilities';
import { IListDesignation } from './Designation';
import { IListEmployeeStatus } from './EmployeeStatus';
import { IListEmployeeType } from './EmployeeType';
import { emit } from 'cluster';

export interface IEmployee {
    id?: number,
    lastName: string,
    firstName: string,
    phone: string,
    mobilePhone: string,
    officialEmail: string,
    email: string,
    employeeStatus: string,
    employeeStatusId?: number,
    designation: string,
    designationId: number,
    employeeType: string,
    employeeTypeId: number,
    hiredAt?: string,
    leaveAt?: string,
    endedProbationAt?: string,
    isUnderProbation: boolean,
    remarks: string,
    street: string,
    state: string,
    city: string,
    zipCode: string,
    country: string,
    abaAccountName: string,
    abaAccountNumber: string,
    statusColorClassName: string,
    employeeTypeColorClassName: string
}

export interface IAddEmployee {
    lastName: string,
    firstName: string,
    phone: string,
    mobilePhone: string,
    officialEmail: string,
    email: string,
    employeeStatusId: number,
    designationId: number,
    employeeTypeId: number,
    abaAccountName: string,
    abaAccountNumber: string
}

export interface IListEmployee {
    id?: number,
    lastName: string,
    firstName: string,
    phone: string,
    mobilePhone: string,
    officialEmail: string,
    email: string,
    employeeStatus: string,
    employeeType: string,
    designation: string,
    statusColorClassName: string,
    employeeTypeColorClassName: string
}

export interface IInitAddEmployee {
    designations: Array<IListDesignation>,
    statuses: Array<IListEmployeeStatus>,
    types: Array<IListEmployeeType>
}

export default class Employees {
    addInit() {
        return RestUtilities.get<IInitAddEmployee>('/api/employees/init');
    }

    fetchAll() {
        return RestUtilities.get<Array<IListEmployee>>('/api/employees');
    }

    fetch(employeeId: number) {
        return RestUtilities.get<IEmployee>(`/api/employees/${employeeId}`);
    }

    search(query: string) {
        return RestUtilities.get<Array<IListEmployee>>(`/api/employees/search/?=${query}`);
    }

    update(employee: IEmployee) {
        console.log("emp update");
        return RestUtilities.put<IEmployee>(`/api/employees/${employee.id}`, employee);
    }

    create(employee: IAddEmployee) {
        console.log("emp create");
        console.log(employee);
        return RestUtilities.post<IAddEmployee>('/api/employees', employee);
    }

    delete(employeeId: number) {
        return RestUtilities.delete(`/api/employees/${employeeId}`);
    }
}