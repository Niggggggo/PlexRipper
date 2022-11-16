import Log from 'consola';
import { Observable, of } from 'rxjs';
import { map, switchMap, tap } from 'rxjs/operators';
import { Context } from '@nuxt/types';
import { createAccount, deleteAccount, getAccount, getAllAccounts, updateAccount } from '@api/accountApi';
import { PlexAccountDTO } from '@dto/mainApi';
import { BaseService, GlobalService } from '@service';
import IStoreState from '@interfaces/service/IStoreState';
import ResultDTO from '@dto/ResultDTO';

export class AccountService extends BaseService {
	// region Constructor and Setup

	public constructor() {
		super('AccountService', {
			// Note: Each service file can only have "unique" state slices which are not also used in other service files
			stateSliceSelector: (state: IStoreState) => {
				return {
					accounts: state.accounts,
				};
			},
		});
	}

	public setup(nuxtContext: Context, callBack: (name: string) => void): void {
		super.setNuxtContext(nuxtContext);

		GlobalService.getAxiosReady()
			.pipe(switchMap(() => this.fetchAccounts()))
			.subscribe(() => callBack('AccountService'));
	}

	// endregion

	// region Fetch

	public fetchAccounts(): Observable<PlexAccountDTO[]> {
		return getAllAccounts().pipe(
			switchMap((accountResult) => of(accountResult?.value ?? [])),
			tap((accounts) => {
				Log.debug(`AccountService => Fetch Accounts`, accounts);
				this.setState({ accounts }, 'Fetch Accounts');
			}),
		);
	}

	public fetchAccount(accountId: Number): Observable<PlexAccountDTO | null> {
		return getAccount(accountId).pipe(
			switchMap((accountResult) => of(accountResult?.value ?? null)),
			tap((account) => {
				if (account) {
					Log.debug(`AccountService => Fetch Account`, account);
					this.updateStore('accounts', account);
				}
			}),
		);
	}

	// endregion

	public getAccounts(): Observable<PlexAccountDTO[]> {
		return this.stateChanged.pipe(switchMap((x) => of(x?.accounts ?? [])));
	}

	public getAccount(accountId: number): Observable<PlexAccountDTO | null> {
		return this.getAccounts().pipe(map((x) => x?.find((x) => x.id === accountId) ?? null));
	}

	public createPlexAccount(account: PlexAccountDTO): Observable<PlexAccountDTO | null> {
		return createAccount(account).pipe(
			map((accountResult): PlexAccountDTO | null => accountResult?.value ?? null),
			tap((createdAccount) => {
				if (createdAccount) {
					return this.updateStore('accounts', createdAccount);
				}
				Log.error(`Failed to create account ${account.displayName}`, createdAccount);
			}),
			switchMap((newAccount) => this.getAccount(newAccount?.id ?? 0)),
		);
	}

	public updatePlexAccount(account: PlexAccountDTO, inspect: boolean = false): Observable<PlexAccountDTO | null> {
		return updateAccount(account, inspect).pipe(
			map((accountResult): PlexAccountDTO | null => accountResult?.value ?? null),
			tap((updatedAccount) => {
				if (updatedAccount) {
					return this.updateStore('accounts', updatedAccount);
				}
				Log.error(`Failed to update account ${account.displayName}`, updatedAccount);
			}),
			switchMap((newAccount) => this.getAccount(newAccount?.id ?? 0)),
		);
	}

	public deleteAccount(accountId: number) {
		return deleteAccount(accountId).pipe(switchMap(() => this.fetchAccounts()));
	}
}

const accountService = new AccountService();
export default accountService;
