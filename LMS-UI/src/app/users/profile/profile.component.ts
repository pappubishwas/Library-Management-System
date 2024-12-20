import { Component } from '@angular/core';
import { ApiService } from '../../shared/services/api.service';
import { Role } from '../../models/models';

export interface TableElement {
  name: string;
  value: string;
}

@Component({
  selector: 'profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
})
export class ProfileComponent {
  columns: string[] = ['name', 'value'];
  dataSource: TableElement[] = [];

  constructor(private apiService: ApiService) {
    let user = apiService.getUserInfo()!;
    //console.log(user);
    this.dataSource = [
      { name: "Name", value: user.firstName + " " + user.lastName },
      { name: "Email", value: `${user.email}` },
      { name: "Mobile", value: `${user.mobileNumber}` },
      {name: "Account Status", value: user.isActive ? "Active" : "Inactive" },
      { name: "Created On", value: `${user.createdOn}` },
      { name: "Type", value: `${Role[user.role]}` },
    ];
  }
}