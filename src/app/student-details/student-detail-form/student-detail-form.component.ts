import { Component, OnInit } from '@angular/core';
import { studentDetailService } from 'src/app/shared/student-detail.service';
import { NgForm } from '@angular/forms';
import { studentDetail } from 'src/app/shared/student-detail.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-student-detail-form',
  templateUrl: './student-detail-form.component.html',
  styles: [
  ]
})
export class studentDetailFormComponent implements OnInit {

  constructor(public service: studentDetailService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
    if(this.service.formData.age >= 5 && this.service.formData.age <= 30) {
      if( this.service.formData.percentage >= 10 && this.service.formData.percentage <= 100) {
        if (this.service.formData.id == 0)
          this.insertRecord(form);
        else
          this.updateRecord(form);
      }
      else {
        alert("Please enter percentage in range of 10 to 100");
      }
    }
    else {
      alert("Please enter age in range of 5 to 30");
    }
  }

  insertRecord(form: NgForm) {
    this.service.poststudentDetail().subscribe(
      res => {
        console.log(res);
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Submitted successfully', 'Student Detail Register')
      },
      err => { console.log(err); }
    );
  }

  updateRecord(form: NgForm) {
    this.service.putstudentDetail().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.info('Updated successfully', 'Student Detail Register')
      },
      err => { console.log(err); }
    );
  }


  resetForm(form: NgForm) {
    console.log("Resetting Form");
    form.form.reset();
    this.service.formData = new studentDetail();
  }

}
