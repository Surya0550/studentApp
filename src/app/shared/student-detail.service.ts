import { Injectable } from '@angular/core';
import { studentDetail } from './student-detail.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class studentDetailService {

  constructor(private http: HttpClient) { }

  readonly baseURL = 'http://localhost:5000/api/student'
  formData: studentDetail = new studentDetail();
  list: studentDetail[];

  poststudentDetail() {
    return this.http.post(this.baseURL, this.formData);
  }

  putstudentDetail() {
    return this.http.put(`${this.baseURL}/${this.formData.id}`, this.formData);
  }

  deletestudentDetail(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

  refreshList() {
    this.http.get(this.baseURL)
      .toPromise()
      .then(res =>this.list = JSON.parse(JSON.stringify(res)) as studentDetail[]);
  }


}
