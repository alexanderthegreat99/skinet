import {inject} from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs';
 
import { AccountService } from 'src/app/account/account.service';
 
export const authGuard = () => {
  const accountService = inject(AccountService);
  const router = inject(Router);
  // console.log("authGuard invoked");
 
  return accountService.currentUser$.pipe(
    map(auth => {
      if (auth) {
        // console.log("allow route");
        return true;
      }

      else {
        // console.log("dont allow route");
        router.navigate(['/account/login'], {queryParams: {returnUrl: router.url}});
        return false
      }
    })
  );
}; 