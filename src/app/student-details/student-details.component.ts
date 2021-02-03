import { Component, OnInit } from '@angular/core';
import { studentDetailService } from '../shared/student-detail.service';
import { studentDetail } from '../shared/student-detail.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-student-details',
  templateUrl: './student-details.component.html',
  styles: [
  ]
})
export class studentDetailsComponent implements OnInit {

  constructor(public service: studentDetailService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord: studentDetail) {
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id: number) {
    if (confirm('Are you sure to delete this record?')) {
      this.service.deletestudentDetail(id)
        .subscribe(
          res => {
            this.service.refreshList();
            this.toastr.error("Deleted successfully", 'Student Detail Register');
          },
          err => { console.log(err) }
        )
    }
  }

}
