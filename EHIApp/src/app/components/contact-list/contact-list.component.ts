import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Contact } from 'src/app/models/contact';
import { ContactService } from 'src/app/services/contact.service';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.scss'],
  providers: [ContactService]
})
export class ContactListComponent implements OnInit {

  contacts: Contact[] = [];
  form: FormGroup;
  mode : number = 1; // create:1, Edit:2, View: 3
  @ViewChild('showModalBtn', { static: true }) showModalBtn: ElementRef;
  @ViewChild('hideModalBtn', { static: true }) hideModalBtn: ElementRef;

  constructor(private contactService: ContactService, private router: Router) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      Id: new FormControl(0),
      FirstName: new FormControl('', [Validators.required]),
      LastName: new FormControl('', Validators.required),
      Email: new FormControl('', [Validators.required, Validators.email]),
      Phone: new FormControl('', Validators.required)
    });
    this.getContacts();
  }

  get f() {
    return this.form.controls;
  }

  getContacts() {
    this.contactService.getAll().subscribe((data: Contact[]) => {
      this.contacts = data;
      console.log(this.contacts);
    });
  }

  deleteContact(id) {
    this.contactService.delete(id).subscribe(res => {
      this.contacts = this.contacts.filter(item => item.id !== id);
      console.log('Contact deleted successfully!');
    })

  }

  changeStatus(id) {
    this.contactService.changeStatus(id).subscribe(res => {
      let contact = this.contacts.find(item => item.id == id);
      contact.isActive = !contact.isActive ;
      console.log('Sttaus changed successfully!');
    })

  }

  submit() {
    console.log(this.form.value);
    let formValues = this.form.value;
    if (formValues.Id > 0) {
      this.contactService.update(formValues.Id, this.form.value).subscribe(res => {
        console.log('Contact updated successfully!');
        this.hideModalBtn.nativeElement.click();
        this.getContacts();
      });
    }
    else {
      this.contactService.create(this.form.value).subscribe(res => {
        console.log('Contact created successfully!');
        this.hideModalBtn.nativeElement.click();
        this.getContacts();
      });
    }
  }

  openModal(mode : number, contact: Contact) {
    this.form.reset();
    this.mode = mode;
    if(mode != 1){
      this.form.controls['FirstName'].setValue(contact.firstName);
      this.form.controls['LastName'].setValue(contact.lastName);
      this.form.controls['Email'].setValue(contact.email);
      this.form.controls['Phone'].setValue(contact.phone);
      this.form.controls['Id'].setValue(contact.id);
    }
    this.showModalBtn.nativeElement.click();
  }

}
