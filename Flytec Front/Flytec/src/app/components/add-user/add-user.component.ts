import { Component } from '@angular/core';
import { Tutorial } from '../../models/tutorial.model';
import { UsersService } from '../../services/users/users.service';

@Component({
  selector: 'app-add-tutorial',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddTutorialComponent {

  tutorial: Tutorial = {
    nome: '',
    email: '',
  };
  submitted = false;

  constructor(private tutorialService: UsersService) { }

  saveTutorial(): void {
    const data = {
      title: this.tutorial.nome,
      description: this.tutorial.email
    };

    this.tutorialService.create(data)
      .subscribe({
        next: (res) => {
          console.log(res);
          this.submitted = true;
        },
        error: (e) => console.error(e)
      });
  }

  newTutorial(): void {
    this.submitted = false;
    this.tutorial = {
      nome: '',
      email: '',
    };
  }

}