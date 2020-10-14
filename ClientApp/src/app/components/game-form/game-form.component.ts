import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { GameService } from '../../services/game.service';
import { GameListComponent } from '../game-list/game-list.component';
import { Game } from '../../models/game';

@Component({
  templateUrl: './game-form.component.html'
})

export class GameFormComponent implements OnInit {
  isValidFormSubmitted: boolean = false;
  isEditPage: boolean = false;
  pageTitle: string;
  pageDesc: string;
  errorMessage: any;
  allPlatforms: string[];
  game = new Game();

  constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
    private _gameService: GameService, private _router: Router)
  {
    if (this._avRoute.snapshot.params["id"]) {
      this.game.id = this._avRoute.snapshot.params["id"];
      this.isEditPage = true;
    }
  }

  ngOnInit(): void {
    if (this.isEditPage) {
      this.pageTitle = "Edit";
      this.pageDesc = "Modify details to change existing game.";
      this._gameService.getGameById(this.game.id)
        .subscribe((resp) => {
          this.game = resp;
        }, error => this.errorMessage = error)
    }
    else {
      this.pageTitle = "New";
      this.pageDesc = "Enter details to create a new game.";
    }

    this.allPlatforms = [
      'PC', 'Xbox One', 'PS4'
    ]
  }

  onFormSubmit(form: NgForm) {
    if (!form.valid) {
      return;
    }

    this.isValidFormSubmitted = true;

    this.game.title = form.controls['title'].value;
    this.game.platforms = form.controls['selectedPlatforms'].value;

    if (this.isEditPage) {
      this._gameService.updateGame(this.game)
        .subscribe((data) => {
          this._router.navigate(['**']);
        }, error => this.errorMessage = error)
    }
    else {
      this._gameService.addGame(this.game)
        .subscribe((data) => {
          this._router.navigate(['**']);
        }, error => this.errorMessage = error)
    }
  }

  cancel() {
    this._router.navigate(['**']);
  }
}

export class Platform {
  constructor(public id: number, public name: string) { }
}

