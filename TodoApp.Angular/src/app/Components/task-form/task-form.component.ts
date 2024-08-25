import { Component, OnInit } from '@angular/core';
import {  FormBuilder, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { AuthService } from '../../Services/auth.service';
import { ContentService } from '../../Services/content.service';
import { NavService } from '../../Services/nav.service';
import { ToastService } from '../../Services/toast.service';
import { TaskRegistration } from '../../Models/TaskRegistration';
import { TaskView } from '../../Models/TaskView';
import { NonNullAssert } from '@angular/compiler';
import { StatusView } from '../../Models/StatusView';
import { PriorityView } from '../../Models/PriorityView';
import { error } from 'console';
import { TaskDetails } from '../../Models/TaskDetails';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './task-form.component.html',
  styleUrl: './task-form.component.scss'
})
export class TaskFormComponent implements OnInit{

  editMode = false;
  taskDetails!: TaskDetails;
  taskForm!: FormGroup;
  statusList:StatusView[]=[];
  priorityList:PriorityView[]=[];

  constructor(
    public navService: NavService,
    private formBuilder: FormBuilder,
    public contentService: ContentService,
    public authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.taskForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', [Validators.required, Validators.minLength(3)]],
      statusId: [0, [Validators.required, Validators.min(1)]],
      priorityId: [0, [Validators.required, Validators.min(1)]]
    });
    this.contentService.getAllPriorites().subscribe({
      next:(value)=>{
        this.priorityList=value;
      }
    })
    this.contentService.getAllStatus().subscribe({
      next:(value)=>{
        this.statusList=value;
      }
    })


    this.contentService.editTaskSubject.subscribe({
      next: (value:string ) => {
        if (value!== "") {
          this.navService.isFormDisplay = true;
          this.editMode = true;
          this.contentService.getTaskDetails(value).subscribe({
            next:(value)=>{
              this.taskForm.patchValue(value);
              this.taskDetails=value;
            },
            error:(err)=>{
              this.contentService.handleError(err.statusCode,err.message);
            }
          })          
        }
      }
    });
  }

  onSubmit(): void {
    if (this.taskForm.valid) {
      if (this.editMode) {
        this.contentService.editTask(this.taskForm.value as TaskRegistration, this.taskDetails.id).subscribe({
          next: () => {
            this.handleFormSubmission('Task Edited');
          }
        });
      } else {
        this.contentService.addTask(this.taskForm.value as TaskRegistration).subscribe({
          next: () => {
            this.handleFormSubmission('Task Created');
          }
        });
      }
    } else {
      this.checkValidation();
    }
  }

  onCancel(): void {
    this.navService.toggleForm();
  }

  checkValidation(): void {
    const formControls = this.taskForm.controls;

    if (formControls['name'].hasError('minlength')) {
      this.toastService.showWarn('Task title is too short');
    }
    if (formControls['description'].hasError('required')) {
      this.toastService.showWarn('Description is required');
    }
    if (formControls['name'].hasError('required')) {
      this.toastService.showWarn('Title is required');
    }
    if (formControls['description'].hasError('minlength')) {
      this.toastService.showWarn('Description is too short');
    }
    if (formControls['statusId'].hasError('min')) {
      this.toastService.showWarn('Select a valid Status Value');
    }
    if (formControls['priorityId'].hasError('min')) {
      this.toastService.showWarn('Select a valid Priority Value');
    }
  }

  private handleFormSubmission(message: string): void {
    this.navService.isFormDisplay = false;
    this.contentService.dataUpdatedSubject.next(1);
    this.router.navigate([this.activatedRoute.snapshot.url[0].path]);
    this.toastService.showInfo(message);
    this.editMode=false;
  }
}