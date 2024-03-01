import { test , expect } from "@playwright/test";

test.describe('Load logg inn page', () => {
  test('has title', async ({ page }) => {
    await page.goto('/');

    // Expect a title "Sign in to your account" when no authorization.
    await expect(page).toHaveTitle(/Sign in to your account/);
  });
});
test.describe('/getUser endpoint', () => {
  test('GET - current user', async ({ request }) => {
    const response = await request.get('/client/User');
    await expect(response).toBeOK();

  });
});