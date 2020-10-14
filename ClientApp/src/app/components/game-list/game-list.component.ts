import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { GameService } from '../../services/game.service'
import { Game } from '../../models/game';

@Component({
  templateUrl: './game-list.component.html'
})

export class GameListComponent {
  public games: Game[];

  constructor(public http: HttpClient, private _router: Router, private _gameService: GameService) {
    this.getGames();
  }

  getGames() {
    this._gameService.getGames().subscribe(
      data => this.games= data
    )
  }

  deleteGame(gameId) {
    var ans = confirm("Do you want to delete this game?");
    if (ans) {
      this._gameService.deleteGame(gameId).subscribe((data) => {
        this.getGames();
      }, error => console.error(error))
    }
  }
}
