import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../user.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(
    private router: Router,
    private userService: UserService
  ){}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot){
    const user = this.userService.userValue;
    if(user){
      if (user.role=="admin")
      return true;
    }
    
    alert("You don't have admin rights")
    return false;
  }
  
}
